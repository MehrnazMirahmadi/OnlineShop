namespace OnlineShop.API.Proxies;

public interface ITrackingCodeProxy
{
    public Task<string> Get(CancellationToken cancellationToken);
}