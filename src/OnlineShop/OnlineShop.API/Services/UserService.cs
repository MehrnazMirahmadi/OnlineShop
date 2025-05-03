using Mehrnaz.Extensions;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.API.Exceptions;

namespace OnlineShop.API.Services
{
    public class UserService(IUserRepository _userRepository,IMemoryCache memoryCache) : IUserService
    {
      
        public async Task<List<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            const string cacheKey = "AllUsers";

            if (!memoryCache.TryGetValue(cacheKey, out List<UserDTO> cachedUsers))
            {
                var users = await _userRepository.GetAllUsersAsync(cancellationToken);

                cachedUsers = users.Select(u => new UserDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    NationalCode = u.NationalCode,
                    FullName = $"{u.FirstName} {u.LastName}"
                }).ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                memoryCache.Set(cacheKey, cachedUsers, cacheEntryOptions);
            }

            return cachedUsers;
        }
        public async Task<List<UserDTO>> GetUserByNameAsync(string username, CancellationToken cancellationToken)
        {
            var cacheKey = $"users_by_name_{username}";

            if (memoryCache.TryGetValue(cacheKey, out List<UserDTO> cachedUsers))
            {
                return cachedUsers;
            }

            var users = await _userRepository.GetUsersByNameAsync(username, cancellationToken);
            if (users is null || !users.Any()) throw new UserNotFoundException($"کاربری با نام {username} پیدا نشد.");

            var userDTOs = users.Select(user => new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                NationalCode = user.NationalCode,
                FullName = $"{user.FirstName} {user.LastName}"
            }).ToList();

            memoryCache.Set(cacheKey, userDTOs, TimeSpan.FromMinutes(10));

            return userDTOs;
        }

        public async Task CreateUserAsync(CreateUserDTO userDTO, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PhoneNumber = userDTO.PhoneNumber,
                NationalCode = userDTO.NationalCode,            
                Password = userDTO.Password.ToSha256(),
            };

            await _userRepository.CreateUserAsync(user, cancellationToken);
        }

        public async Task UpdateUserAsync(UpdateUserDTO userDTO, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userDTO.Id, cancellationToken);
            if (existingUser == null) return;

            existingUser.FirstName = userDTO.FirstName;
            existingUser.LastName = userDTO.LastName;
            existingUser.PhoneNumber = userDTO.PhoneNumber;  
            existingUser.Password = userDTO.Password.ToSha256();
            existingUser.NationalCode = userDTO.NationalCode;

            await _userRepository.UpdateUserAsync(existingUser, cancellationToken);
        }

        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteUserAsync(id, cancellationToken);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);

            if (user is null)
                return null;

            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                NationalCode = user.NationalCode,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }

    }
}
