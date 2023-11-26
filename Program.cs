using System.Text.Json;

namespace Shelly;

public static class Program {

    public static async Task Main() {
        var client = new HttpClient();

        var shelly = new Shelly25 { BaseUri = "http://192.168.178.47" };

        foreach (var channel in shelly.Channels) {
            Console.WriteLine($"Channel {channel.Index}:");

            var relayData = await channel.GetRelayAsync(client);
            PrettyPrint(relayData);

            var meterData = await channel.GetMeterAsync(client);
            PrettyPrint(meterData);

            var settingsData = await channel.GetRelaySettingsAsync(client);
            PrettyPrint(settingsData);
        }

        // var f = await client.GetAsync("http://192.168.178.47/settings/relay/0");
        // var c = await f.Content.ReadAsStringAsync();
        // System.Console.WriteLine(c);

        await PrintStatus(client, shelly, channelNumber: 0);
        await PrintStatus(client, shelly, channelNumber: 1);
    }

    static async Task PrintStatus(HttpClient client, Shelly25 shelly, int channelNumber) {
        var channel = shelly.Channels[channelNumber];

        var settings = await channel.GetRelaySettingsAsync(client);
        var meter = await channel.GetMeterAsync(client);

        Console.WriteLine($"{settings?.Name}: {AnAus(settings?.IsOn)}, Power: {meter?.Power}W");

        string AnAus(bool? value) {
            return value == true ? "An" : "Aus";
        }
    }

    static void PrettyPrint<T>(T o) {
        var s = FormatAsJson(o);
        Console.WriteLine(s);
    }

    static string FormatAsJson<T>(T o) {
        var s = JsonSerializer.Serialize(o, new JsonSerializerOptions { WriteIndented = true });
        return s;
    }

}
