using OnlineShop.API.Repository;

namespace OnlineShop.API.Data;

public class UnitOfWork
    (OnlineShopDbContext db, IUserRepository userRepository, ICityRepository cityRepository)
: IUnitOfWork
{
    public IUserRepository userRepository { get ; init; } = userRepository;
    public ICityRepository cityRepository { get; init; } = cityRepository;

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await db.SaveChangesAsync(cancellationToken) > 0;
    }

}
