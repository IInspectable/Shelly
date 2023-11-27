using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Shelly;

public record MeterData {

    [JsonPropertyName("power")]
    public double Power { get; init; }

    [JsonPropertyName("overpower")]
    public double OverPower { get; init; }

    [JsonPropertyName("is_valid")]
    public bool IsValid { get; init; }

    [JsonPropertyName("timestamp")]
    public int Timestamp { get; init; }

    [JsonPropertyName("counters")]
    public ImmutableList<double> Counters { get; init; } = ImmutableList<double>.Empty;

    [JsonPropertyName("total")]
    public int Total { get; init; }

}