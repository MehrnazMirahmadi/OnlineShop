using OnlineShop.API.DTOs;

namespace OnlineShop.API.Services;

public interface IUserService
{
    Task<UserDTO> GetUserByNameAsync(string username);
    Task<List<UserDTO>> GetAllUsersAsync();
    Task CreateUserAsync(CreateUserDTO userDTO);
    Task UpdateUserAsync(UpdateUserDTO userDTO);
    Task DeleteUserAsync(int id);
}

