# :bell: DingDong

**DingDong** is a .NET 10 PoC library that implements a simple in-memory pub-sub pattern, allowing multiple asynchronous flows to continuously stream events sent by a central authority.

## :grey_question: Why ?

The initial goal was to provide simple and straightforward APIs for making server-sent event HTTP endpoints, by allowing the consumer to easily publish events to multiple asynchronous flows that would continuously consume these events.

Samples are available to demonstrate the usage inside an ASP.NET Minimal API SSE endpoint.

## :rocket: How-to

### :gear: Setup

Add services to the DI :

```c#
builder.Services.AddPubSub<SomeEvent>();
```

### :mailbox_with_no_mail: Subscribe

```c#
var subManager = servicesProvider.GetRequiredService<ISubscriptionManager<SomeEvent>>();
await foreach (SomeEvent message in subManager.SubscribeAsync())
{
    // Process the message here
}
```

### :incoming_envelope: Publish

```c#
var publisher = servicesProvider.GetRequiredServcie<IPublisher<SomeEvent>>();
await publisher.PublishAsync(new SomeEvent("Some event message"));
```
