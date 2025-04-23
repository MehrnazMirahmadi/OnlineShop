using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.Services;

namespace OnlineShop.API.Controllers
{
    [Route("api/OnlineShop")]
    [ApiController]
    public class OnlineShopController(IUserService _userService) : ControllerBase
    {
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("users/{username}")]
        public async Task<ActionResult<UserDTO>> GetUserByName(string username)
        {
            var user = await _userService.GetUserByNameAsync(username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
        {
            await _userService.CreateUserAsync(dto);
            return Ok(new { message = "User created successfully." });
        }

        [HttpPut("users")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto)
        {
            await _userService.UpdateUserAsync(dto);
            return Ok(new { message = "User updated successfully." });
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(new { message = "User deleted successfully." });
        }
    }
}
