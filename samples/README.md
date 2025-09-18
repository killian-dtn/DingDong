# Samples

Server and client side samples showcasing the usage of the library in an HTTP SSE endpoint.

## Samples.Server.MinimalApi

Server-side app, defines 2 endpoints :

- `GET /subscribe` for subscribing to events
- `POST /start-job` for starting a fake job (takes an `s` query argument defining the job duration in seconds)
    - First emits a `job-started` event
    - After `s` seconds, emits a `job-ended` event

## Samples.Client.BlazorWasm

Client-side app, subscribes to the `/subscribe` endpoint via the `EventSource` JavaScript API and shows streamed events in the browser.

## Try

1. Launch the API

> dotnet run --project .\Samples.Server.MinimalApi\

2. Launch the Blazor client

> dotnet run --project .\Samples.Client.BlazorWasm\

3. Use the `/start-job` API endpoint to start a fake job (a `/scalar` page is available on the API)

> curl 'https://localhost:7286/start-job?s=5' --request POST

4. Watch the Blazor client updating
