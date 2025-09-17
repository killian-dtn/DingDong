using Microsoft.Extensions.DependencyInjection;

namespace Kdant.DingDong;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPubSub<TMessage>(this IServiceCollection services)
    {
        return services
            .AddSingleton<Publisher<TMessage>>()
            .AddSingleton<IPublisher<TMessage>>(sp => sp.GetRequiredService<Publisher<TMessage>>())
            .AddSingleton<SubscriptionManager<TMessage>>()
            .AddSingleton<ISubscriptionManager<TMessage>>(sp => sp.GetRequiredService<SubscriptionManager<TMessage>>())
            .AddHostedService<MessageDispatchWorker<TMessage>>();
    }
}
