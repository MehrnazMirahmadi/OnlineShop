using OnlineShop.API.Features;
using System.Linq.Expressions;

namespace OnlineShop.API.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<List<User>> GetUsersByNameAsync(string username, CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateUserAsync(User user, CancellationToken cancellationToken);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken);
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
        Task SoftDeleteUserAsync(int id, CancellationToken cancellationToken);
        Task<List<User>> ListAsync(BaseSpecification<User> spec, CancellationToken cancellationToken);
        Task<int> CountAsync(Expression<Func<User, bool>>? criteria, CancellationToken cancellationToken);

    }
}
