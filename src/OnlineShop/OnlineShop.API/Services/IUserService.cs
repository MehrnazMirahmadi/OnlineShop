using OnlineShop.API.ViewModel;

namespace OnlineShop.API.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUserByNameAsync(string username, CancellationToken cancellationToken);
        Task CreateUserAsync(CreateUserDTO userDTO, CancellationToken cancellationToken);
        Task UpdateUserAsync(UpdateUserDTO userDTO, CancellationToken cancellationToken);
        Task SoftDeleteUserAsync(int id, CancellationToken cancellationToken);
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
        Task<UserViewModel> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
