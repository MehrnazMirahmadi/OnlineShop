namespace OnlineShop.API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserByNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
