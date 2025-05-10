namespace OnlineShop.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
      

        [HttpGet("{username}")]
        public async Task<ActionResult<UserDTO>> GetUserByName(string username, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByNameAsync(username, cancellationToken);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto, CancellationToken cancellationToken)
        {
            await _userService.CreateUserAsync(dto, cancellationToken);
            return Ok(new { message = "User created successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(dto, cancellationToken);
            return Ok(new { message = "User updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(id, cancellationToken);
            return Ok(new { message = "User deleted successfully." });
        }
    }
}
