using System.Threading.Channels;

namespace Kdant.DingDong;

public interface IPublisher<TMessage>
{
    Task PublishAsync(TMessage message);
}

internal sealed class Publisher<TMessage> : IPublisher<TMessage>
{
    private readonly Channel<TMessage> _channel;
    internal ChannelReader<TMessage> Reader => _channel.Reader;

    public Publisher()
    {
        _channel = Channel.CreateUnbounded<TMessage>();
    }

    public async Task PublishAsync(TMessage message)
    {
        await _channel.Writer.WriteAsync(message);
    }
}
