using System.Collections.Immutable;
using System.Net.Http.Json;
// https://shelly-api-docs.shelly.cloud/gen1/#shelly2-5

namespace Shelly;

public class Shelly25 {
    public Shelly25() {
        Channels = [
            new Channel { Shelly = this, Index = 0 },
            new Channel { Shelly = this, Index = 1 }
            ];
    }

    public required string BaseUri { get; init; }

    public Channel Channel0 => Channels[0];
    public Channel Channel1 => Channels[1];

    public ImmutableArray<Channel> Channels { get; }

    public class Channel {

        public required Shelly25 Shelly { get; init; }
        public required int Index { get; init; }

        public string RelayUri => $"{Shelly.BaseUri}/relay/{Index}";
        public string MeterUri => $"{Shelly.BaseUri}/meter/{Index}";
        public string RelaySettingsUri => $"{Shelly.BaseUri}/settings/relay/{Index}";

        public async Task<RelayData?> GetRelayAsync(HttpClient client) => await GetAsync<RelayData>(client, RelayUri);
        public async Task<MeterData?> GetMeterAsync(HttpClient client) => await GetAsync<MeterData>(client, MeterUri);
        public async Task<RelaySettingsData?> GetRelaySettingsAsync(HttpClient client) => await GetAsync<RelaySettingsData>(client, RelaySettingsUri);
    }

    static async Task<T?> GetAsync<T>(HttpClient client, string requestUri) {
        var response = await client.GetAsync(requestUri);
        var json = await response.Content.ReadFromJsonAsync<T>();
        return json;
    }
}
