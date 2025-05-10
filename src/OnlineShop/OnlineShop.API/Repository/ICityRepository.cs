namespace OnlineShop.API.Repository;

public interface ICityRepository
{
    public Task<List<City>> GetListCitiesAsync(CancellationToken cancellationToken);
}
