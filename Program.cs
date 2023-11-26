using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

public static class Program
{

    public static async Task Main()
    {
        var client = new HttpClient();

        var shelly = new Shelly { BaseUri = "http://192.168.178.47" };

        // foreach (var channel in shelly.Channels)
        // {
        //     Console.WriteLine($"Channel {channel.Index}:");

        //     var relayData = await channel.GetRelayAsync(client);
        //     PrettyPrint(relayData);

        //     var meterData = await channel.GetMeterAsync(client);
        //     PrettyPrint(meterData);

        //     var settingsData = await channel.GetRelaySettingsAsync(client);
        //     PrettyPrint(settingsData);
        // }

        // var f = await client.GetAsync("http://192.168.178.47/settings/relay/0");
        // var c = await f.Content.ReadAsStringAsync();
        // System.Console.WriteLine(c);

        await PrintStatus(client, shelly, channelNumber: 0);
        await PrintStatus(client, shelly, channelNumber: 1);
    }

    static async Task PrintStatus(HttpClient client, Shelly shelly, int channelNumber)
    {
        var channel = shelly.Channels[channelNumber];

        var settings = await channel.GetRelaySettingsAsync(client);
        var meter = await channel.GetMeterAsync(client);

        Console.WriteLine($"{settings?.Name}: {AnAus(settings?.IsOn)}, Power: {meter?.Power}W");

        string AnAus(bool? value)
        {
            return value == true ? "An" : "Aus";
        }
    }

    static void PrettyPrint<T>(T o)
    {
        var s = FormatAsJson(o);
        Console.WriteLine(s);
    }

    static string FormatAsJson<T>(T o)
    {
        var s = JsonSerializer.Serialize(o, new JsonSerializerOptions { WriteIndented = true });
        return s;
    }

}

// https://shelly-api-docs.shelly.cloud/gen1/#shelly2-5
public class Shelly
{
    public Shelly()
    {
        Channels = [
            new Channel { Shelly = this, Index = 0 },
            new Channel { Shelly = this, Index = 1 }
            ];
    }

    public required string BaseUri { get; init; }

    public Channel Channel0 => Channels[0];
    public Channel Channel1 => Channels[1];

    public ImmutableArray<Channel> Channels { get; }

    public class Channel
    {
        public required Shelly Shelly { get; init; }
        public required int Index { get; init; }
        public string RelayUri => $"{Shelly.BaseUri}/relay/{Index}";
        public string MeterUri => $"{Shelly.BaseUri}/meter/{Index}";
        public string RelaySettingsUri => $"{Shelly.BaseUri}/settings/relay/{Index}";
        public async Task<RelayData?> GetRelayAsync(HttpClient client) => await GetAsync<RelayData>(client, RelayUri);
        public async Task<MeterData?> GetMeterAsync(HttpClient client) => await GetAsync<MeterData>(client, MeterUri);
        public async Task<RelaySettingsData?> GetRelaySettingsAsync(HttpClient client) => await GetAsync<RelaySettingsData>(client, RelaySettingsUri);
    }

    static async Task<T?> GetAsync<T>(HttpClient client, string requestUri)
    {
        var response = await client.GetAsync(requestUri);
        var json = await response.Content.ReadFromJsonAsync<T>();
        return json;
    }
}

public record RelayData
{
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

public record MeterData
{

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

public record RelaySettingsData
{
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