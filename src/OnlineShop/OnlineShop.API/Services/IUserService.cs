namespace OnlineShop.API.Services
{
    public interface IUserService
    {

        Task<List<UserDTO>> GetUserByNameAsync(string username, CancellationToken cancellationToken);
        Task<List<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task CreateUserAsync(CreateUserDTO userDTO, CancellationToken cancellationToken);
        Task UpdateUserAsync(UpdateUserDTO userDTO, CancellationToken cancellationToken);
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
        Task<UserDTO> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
