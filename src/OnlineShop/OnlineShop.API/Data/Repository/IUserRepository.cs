namespace OnlineShop.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<User> GetUserByNameAsync(string username, CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateUserAsync(User user, CancellationToken cancellationToken);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken);
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
    }
}
