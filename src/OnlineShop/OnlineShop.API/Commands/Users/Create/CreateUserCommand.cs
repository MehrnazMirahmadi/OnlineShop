using MediatR;

namespace OnlineShop.API.Commands.Users.Create;

public record CreateUserCommand(UserDTO UserDTO) : IRequest;

