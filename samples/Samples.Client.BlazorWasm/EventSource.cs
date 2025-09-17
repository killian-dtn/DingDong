using Microsoft.JSInterop;

namespace Samples.Client.BlazorWasm;

/// <summary>
/// Simple C# wrapper around the JS EventSourceWrapper class.
/// </summary>
internal class EventSource(IJSObjectReference wrapperInstance) : IAsyncDisposable
{
    public async ValueTask AddListenerAsync<T>(string key, DotNetObjectReference<T> methodInstance, string methodName) where T : class
        => await wrapperInstance.InvokeVoidAsync("addListener", key, methodInstance, methodName);

    public async ValueTask CloseAsync() => await wrapperInstance.InvokeVoidAsync("close");

    public async ValueTask DisposeAsync()
    {
        await CloseAsync();
        await wrapperInstance.DisposeAsync();
    }
}

internal class EventSourceFactory(IJSRuntime js)
{
    public async Task<EventSource> CreateAsync(string url)
    {
        // await using var sse = await js.InvokeAsync<IJSObjectReference>("import", "./sse.js");
        var esWrapper = await js.InvokeAsync<IJSObjectReference>("createEventSourceWrapper", url);
        return new EventSource(esWrapper);
    }
}