using System.Threading.Channels;

namespace Kdant.DingDong;

public sealed class Subscriber<TMessage>
{
    public required Guid Id { get; init; }
    public required Channel<TMessage> Channel { internal get; init; }
}