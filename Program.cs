using System.Text.Json;

namespace Shelly;

public static class Program {

    public static async Task Main() {

        var shelly = new Shelly25 {
            BaseUri    = "http://192.168.178.47",
            HttpClient = new HttpClient()
        };

        // foreach (var channel in shelly.Channels) {
            
        //     Console.WriteLine($"Channel {channel.Index}:");

        //     var relayData = await channel.GetRelayAsync();
        //     PrettyPrint(relayData);

        //     var meterData = await channel.GetMeterAsync();
        //     PrettyPrint(meterData);

        //     var settingsData = await channel.GetRelaySettingsAsync();
        //     PrettyPrint(settingsData);
        // }

        // var f = await client.GetAsync("http://192.168.178.47/settings/relay/0");
        // var c = await f.Content.ReadAsStringAsync();
        // System.Console.WriteLine(c);

        await PrintStatus(shelly.Channels[0]);
        await PrintStatus(shelly.Channels[1]);
    }

    static async Task PrintStatus(Shelly25.Channel channel) {

        var settings = await channel.GetRelaySettingsAsync();
        var meter    = await channel.GetMeterAsync();

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