namespace OnlineShop.API.Services
{
    public class UserService(IUserRepository _userRepository) : IUserService
    {
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserDTO
            {

                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber
            }).ToList();
        }

        public async Task<UserDTO> GetUserByNameAsync(string username)
        {
            var user = await _userRepository.GetUserByNameAsync(username);
            if (user == null) return null;

            return new UserDTO
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task CreateUserAsync(CreateUserDTO userDTO)
        {
            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PhoneNumber = userDTO.PhoneNumber
            };

            await _userRepository.CreateUserAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userDTO.Id);
            if (existingUser == null) return;

            existingUser.FirstName = userDTO.FirstName;
            existingUser.LastName = userDTO.LastName;
            existingUser.PhoneNumber = userDTO.PhoneNumber;

            await _userRepository.UpdateUserAsync(existingUser);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
