using Microsoft.Extensions.Options;

namespace OnlineShop.API.Proxies;

public class TrackingCodeProxy(IOptions<Settings> options, HttpClient httpClient) : ITrackingCodeProxy
{
    private readonly TrackingCodeSettings _settings = options.Value.TrackingCode;

    public async Task<string> Get(CancellationToken cancellationToken)
    {
        var url = string.Format(_settings.GetURL, _settings.Prefix);

        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("TrackingCode not available.");
        }

        response.EnsureSuccessStatusCode();

        var trackingCode = await response.Content.ReadAsStringAsync(cancellationToken);
        return trackingCode;
    }
}