using System.Net.ServerSentEvents;
using Kdant.DingDong;
using Microsoft.AspNetCore.Mvc;
using Samples.Shared;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddOpenApi();
builder.Services.AddPubSub<SomeEvent>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin());
app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/subscribe", static (HttpContext ctx,
    [FromServices] ISubscriptionManager<SomeEvent> subManager) =>
{
    return TypedResults.ServerSentEvents(subManager
        .SubscribeAsync(ctx.RequestAborted)
        .Select(x => new SseItem<SomeEvent>(x, x.EventType)));
});

app.MapPost("/start-job", static async (HttpContext ctx,
    [FromQuery(Name = "s")] int seconds,
    [FromServices] IPublisher<SomeEvent> publisher) =>
{
    // Starts a background job
    _ = Task.Run(async () =>
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        await publisher.PublishAsync(SomeEvent.Ended("Job ended successfully"));
    }, ctx.RequestAborted);

    await publisher.PublishAsync(SomeEvent.Started("Job started successfully"));
    return TypedResults.Ok();
});

app.Run();
