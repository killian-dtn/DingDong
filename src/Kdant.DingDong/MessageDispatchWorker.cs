using Microsoft.Extensions.Hosting;

namespace Kdant.DingDong;

internal sealed class MessageDispatchWorker<TMessage>(
    Publisher<TMessage> publisher,
    SubscriptionManager<TMessage> subManager) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in publisher.Reader.ReadAllAsync(stoppingToken))
        {
            await Task.WhenAll(subManager
                .GetSubscribers()
                .Select(s => s.Channel.Writer
                    .WriteAsync(message)
                    .AsTask()));
        }
    }
}