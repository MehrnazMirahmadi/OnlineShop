namespace OnlineShop.API.Data.Repository
{
    public class UserRepository(OnlineShopDbContext _context) : IUserRepository
    {
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    EF.Functions.Like(u.FirstName, $"%{username}%") ||
                    EF.Functions.Like(u.LastName, $"%{username}%"));
        }

    }
}
