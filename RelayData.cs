using System.Text.Json.Serialization;

namespace Shelly;

public record RelayData {
    [JsonPropertyName("ison")]
    public bool IsOn { get; init; }

    [JsonPropertyName("has_timer")]
    public bool HasTimer { get; init; }

    [JsonPropertyName("timer_started")]
    public int TimerStarted { get; init; }

    [JsonPropertyName("timer_duration")]
    public int TimerDuration { get; init; }

    [JsonPropertyName("timer_remaining")]
    public int TimerRemaining { get; init; }

    [JsonPropertyName("overpower")]
    public bool OverPower { get; init; }

    [JsonPropertyName("over_temperature")]
    public bool OverTemperature { get; init; }

    [JsonPropertyName("is_valid")]
    public bool IsValid { get; init; }

    [JsonPropertyName("source")]
    public string? Source { get; init; }
}
