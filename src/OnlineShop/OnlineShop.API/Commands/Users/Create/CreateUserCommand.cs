using MediatR;

namespace OnlineShop.API.Commands.Users.Create;

public record CreateUserCommand(CreateUserDTO CreateUserDTO) : IRequest;

