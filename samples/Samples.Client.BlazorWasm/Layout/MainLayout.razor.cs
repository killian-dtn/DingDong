using System.Collections.Immutable;
using System.Text.Json;
using Microsoft.JSInterop;
using Samples.Shared;

namespace Samples.Client.BlazorWasm.Layout;

public sealed partial class MainLayout : IAsyncDisposable
{
    private ImmutableList<string> _messages = [];
    private DotNetObjectReference<MainLayout>? _refThis;
    private EventSource? _es;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _refThis = DotNetObjectReference.Create(this);
            _es = await EsFactory.CreateAsync("https://localhost:7286/subscribe");
            await _es.AddListenerAsync(SomeEvent.JobStarted, _refThis, nameof(HandleEvent));
            await _es.AddListenerAsync(SomeEvent.JobEnded, _refThis, nameof(HandleEvent));
        }
    }

    [JSInvokable]
    public void HandleEvent(string data)
    {
        var obj = JsonSerializer.Deserialize(data, SomeEventJsonSerializerContext.Default.SomeEvent)!;
        _messages = _messages.Add($"Handled {obj.EventType} event {obj.SomeId}");
        StateHasChanged();
    }

    public async ValueTask DisposeAsync()
    {
        if (_es is not null)
        {
            await _es.DisposeAsync();
        }

        _refThis?.Dispose();
    }
}