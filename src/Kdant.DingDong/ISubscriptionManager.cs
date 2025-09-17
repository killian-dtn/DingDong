using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace Kdant.DingDong;

public interface ISubscriptionManager<TMessage>
{
    IAsyncEnumerable<TMessage> SubscribeAsync(CancellationToken ct);
}

internal sealed class SubscriptionManager<TMessage> : ISubscriptionManager<TMessage>
{
    private readonly ConcurrentDictionary<Guid, Subscriber<TMessage>> _subs;

    public SubscriptionManager()
    {
        _subs = new ConcurrentDictionary<Guid, Subscriber<TMessage>>();
    }

    public async IAsyncEnumerable<TMessage> SubscribeAsync([EnumeratorCancellation] CancellationToken ct)
    {
        var sub = new Subscriber<TMessage>
        {
            Id = Guid.NewGuid(),
            Channel = Channel.CreateUnbounded<TMessage>(),
        };

        _subs.TryAdd(sub.Id, sub);

        await foreach (var message in sub.Channel.Reader.ReadAllAsync(ct))
        {
            yield return message;
        }
        
        _subs.Remove(sub.Id, out _);
    }

    internal IEnumerable<Subscriber<TMessage>> GetSubscribers() => _subs.Values;
}
