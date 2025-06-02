using MediatR;
using OnlineShop.API.Commands.Users.Create;
using OnlineShop.API.Commands.Users.Delete;
using OnlineShop.API.Commands.Users.Update;
using OnlineShop.API.Enums;
using OnlineShop.API.Features;
using OnlineShop.API.Queries.User.GetByName;
using OnlineShop.API.ViewModel;
using System.Reflection;
//using static OnlineShop.API.ViewModel.EnumExtensions;

namespace OnlineShop.API.Controllers
{

    [Route("api/Users")]
    [ApiController]

    public class UserController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("filter")]
        public async Task<IActionResult> GetUsersByFilter(
    [FromQuery] string? firstName,
    [FromQuery] string? lastName,
    [FromQuery] string? nationalCode,
    [FromQuery] OrderType? orderType,
    [FromQuery] int pageSize = 10,
    [FromQuery] int pageNumber = 1,
    CancellationToken cancellationToken = default)
        {
            var query = new GetUserByNameQuery
            {
                FirstName = firstName,
                LastName = lastName,
                NationalCode = nationalCode,
                OrderType = orderType,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = await _mediator.Send(query, cancellationToken);
            return Ok(BaseResult.Success(result));
        }


        //[HttpGet("{username}")]
        //public async Task<ActionResult<List<UserDTO>>> GetUserByName(string username, CancellationToken cancellationToken)
        //{
        //    var query = new GetUserByNameQuery(username);
        //    var users = await _mediator.Send(query, cancellationToken);
        //    return Ok(BaseResult.Success(users));
        //}

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateUserCommand(dto), cancellationToken);
            return Ok(BaseResult.Success());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateUserCommand(dto), cancellationToken);
            return Ok(BaseResult.Success());
        }

        /*  [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
          {
              await _userService.DeleteUserAsync(id, cancellationToken);
              return Ok(new { message = "User deleted successfully." });
          }
       */
        [HttpPatch("soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteUser(int id, CancellationToken cancellationToken)
        {
             await _mediator.Send(new DeleteCommandUser(id), cancellationToken);
            return Ok(BaseResult.Success("Entity has been deleted"));

        }


    }
}
