using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Shelly;

public record RelaySettingsData {
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("appliance_type")]
    public required string ApplianceType { get; init; }

    [JsonPropertyName("ison")]
    public required bool IsOn { get; init; }

    [JsonPropertyName("has_timer")]
    public required bool HasTimer { get; init; }
    [JsonPropertyName("default_state")]
    public required string DefaultState { get; init; }

    [JsonPropertyName("btn_type")]
    public required string ButtonType { get; init; }

    [JsonPropertyName("btn_reverse")]
    public required int ButtonReverse { get; init; }

    [JsonPropertyName("auto_on")]
    public required double AutoOn { get; init; }

    [JsonPropertyName("auto_off")]
    public required double AutoOff { get; init; }

    [JsonPropertyName("max_power")]
    public required int MaxPower { get; init; }

    [JsonPropertyName("schedule")]
    public required bool Schedule { get; init; }

    [JsonPropertyName("schedule_rules")]
    public required ImmutableList<string> ScheduleRules { get; init; } = ImmutableList<string>.Empty;
}