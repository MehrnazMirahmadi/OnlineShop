using Mehrnaz.Extensions;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.API.Repository;

namespace OnlineShop.Application.Services;

public class UserService(IUserRepository _userRepository, IMemoryCache memoryCache) : IUserService
{
    public async Task<List<UserDTO>> GetUserByNameAsync(string username, CancellationToken cancellationToken)
    {
        var cacheKey = $"users_by_name_{username ?? "all"}";

        if (memoryCache.TryGetValue(cacheKey, out List<UserDTO> cachedUsers))
        {
            return cachedUsers;
        }

        List<User> users;

        if (string.IsNullOrWhiteSpace(username))
        {
            users = await _userRepository.GetAllUsersAsync(cancellationToken);
        }
        else
        {
            users = await _userRepository.GetUsersByNameAsync(username, cancellationToken);

            if (users == null || users.Count == 0)
            {
                users = await _userRepository.GetAllUsersAsync(cancellationToken);
            }
        }

        var userDTOs = users.Select(MapToDTO).ToList();

        memoryCache.Set(cacheKey, userDTOs, TimeSpan.FromMinutes(10));

        return userDTOs;
    }

    public async Task CreateUserAsync(CreateUserDTO userDTO, CancellationToken cancellationToken)
    {
        var hashedPassword = userDTO.Password.ToSha256();

        var user = User.Create(
            userDTO.FirstName,
            userDTO.LastName,
            userDTO.NationalCode,
            userDTO.PhoneNumber,
            hashedPassword
        );

        await _userRepository.CreateUserAsync(user, cancellationToken);
    }

    public async Task UpdateUserAsync(UpdateUserDTO userDTO, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(userDTO.Id, cancellationToken);
        if (user == null) return;

        user.Update(
            userDTO.FirstName,
            userDTO.LastName,
            userDTO.PhoneNumber,
            userDTO.NationalCode,
            userDTO.IsActive,
            userDTO.IsDelete
        );

        if (!string.IsNullOrWhiteSpace(userDTO.Password))
        {
            user.ChangePassword(userDTO.Password.ToSha256());
        }

        await _userRepository.UpdateUserAsync(user, cancellationToken);
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteUserAsync(id, cancellationToken);
    }

    public async Task<UserDTO> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);

        return user == null ? null : MapToDTO(user);
    }

    // --- Mapper Method ---
    private static UserDTO MapToDTO(User user)
    {
        return new UserDTO
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            NationalCode = user.NationalCode,
            FullName = $"{user.FirstName} {user.LastName}"
        };
    }



    public async Task SoftDeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
        if (user == null) return;

        user.SoftDelete();

        await _userRepository.UpdateUserAsync(user, cancellationToken);
    }
}
