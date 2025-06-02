using OnlineShop.API.Data;
using OnlineShop.API.Features;
using OnlineShop.API.Repository;
using System.Linq;
using System.Linq.Expressions;

public class UserRepository(OnlineShopDbContext _dbContext) : IUserRepository
{
    private readonly DbSet<User> _users = _dbContext.Set<User>();

    public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return await _users.ToListAsync(cancellationToken);
    }

    public async Task<List<User>> GetUsersByNameAsync(string username, CancellationToken cancellationToken)
    {
        return await _users
            .Where(u =>
                EF.Functions.Like(u.FirstName, $"%{username}%") ||
                EF.Functions.Like(u.LastName, $"%{username}%"))
            .ToListAsync(cancellationToken);
    }
  

    public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        _users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(id, cancellationToken);
        if (user != null)
        {
            _users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    public async Task SoftDeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(id, cancellationToken);
        if (user == null) return;

        _dbContext.Entry(user).Property("IsDeleted").CurrentValue = true;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<User>> ListAsync(BaseSpecification<User> spec, CancellationToken cancellationToken)
    {
        IQueryable<User> query = _dbContext.Users.AsQueryable();

        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        if (spec.OrderByExpression != null)
            query = query.OrderBy(spec.OrderByExpression);
        else if (spec.OrderByDescendingExpression != null)
            query = query.OrderByDescending(spec.OrderByDescendingExpression);

        if (spec.IsPaginationEnabled)
            query = query.Skip(spec.Skip).Take(spec.Take);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<User, bool>>? criteria, CancellationToken cancellationToken)
    {
        IQueryable<User> query = _dbContext.Users.AsQueryable();

        if (criteria != null)
            query = query.Where(criteria);

        return await query.CountAsync(cancellationToken);
    }

}
