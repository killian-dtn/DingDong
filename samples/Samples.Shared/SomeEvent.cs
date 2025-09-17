using System.Text.Json.Serialization;

namespace Samples.Shared;

[method: JsonConstructor]
public record SomeEvent(Guid SomeId, string EventType, string Message)
{
    public const string JobStarted = "job-started";
    public const string JobEnded = "job-ended";

    public static SomeEvent Started(string message)
        => new SomeEvent(
            SomeId: Guid.NewGuid(),
            EventType: JobStarted,
            Message: message);

    public static SomeEvent Ended(string message)
        => new SomeEvent(
            SomeId: Guid.NewGuid(),
            EventType: JobEnded,
            Message: message);
}

[JsonSourceGenerationOptions(System.Text.Json.JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(SomeEvent))]
public partial class SomeEventJsonSerializerContext : JsonSerializerContext;
