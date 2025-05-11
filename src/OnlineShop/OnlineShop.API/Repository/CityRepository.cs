using OnlineShop.API.Data;

namespace OnlineShop.API.Repository;

public class CityRepository(OnlineShopDbContext _context) : ICityRepository
{
    public async Task<List<City>> GetListCitiesAsync(CancellationToken cancellationToken)
    {
        return await _context.Cities.ToListAsync(cancellationToken);
    }
}
