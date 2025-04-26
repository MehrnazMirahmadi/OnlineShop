namespace OnlineShop.API.Repositories
{
    public class UserRepository(OnlineShopDbContext _dbContext) : IUserRepository
    {
        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> GetUserByNameAsync(string username, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u =>
                    EF.Functions.Like(u.FirstName, $"%{username}%") ||
                    EF.Functions.Like(u.LastName, $"%{username}%"), cancellationToken);
        }


        public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }


        public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await GetUserByIdAsync(id, cancellationToken);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
