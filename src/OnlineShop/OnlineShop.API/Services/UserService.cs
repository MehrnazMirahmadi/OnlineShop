using Mehrnaz.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OnlineShop.API.Services
{
    public class UserService(IUserRepository _userRepository) : IUserService
    {
        public async Task<List<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync(cancellationToken);
            return users.Select(u => new UserDTO
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                FullName = $"{u.FirstName} {u.LastName}"
            }).ToList();
        }

        public async Task<List<UserDTO>> GetUserByNameAsync(string username, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersByNameAsync(username, cancellationToken);
            if (users == null || !users.Any()) return null; // بررسی اینکه آیا کاربری پیدا شده است یا نه

            return users.Select(user => new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                FullName = $"{user.FirstName} {user.LastName}"
            }).ToList();
        }


        public async Task CreateUserAsync(CreateUserDTO userDTO, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PhoneNumber = userDTO.PhoneNumber,
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
            existingUser.PhoneNumber = userDTO.PhoneNumber;  // اصلاح ویرگول اضافی
            existingUser.Password = userDTO.Password.ToSha256();

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
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }

    }
}
