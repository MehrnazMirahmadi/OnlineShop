namespace OnlineShop.API.Data.Repository;

public interface IUserRepository
{
    Task<User> GetUserByNameAsync(string username);
    Task<User> GetUserByIdAsync(int id);
    Task<List<User>> GetAllUsersAsync();
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);


}

