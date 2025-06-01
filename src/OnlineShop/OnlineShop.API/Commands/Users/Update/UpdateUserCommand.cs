using MediatR;

namespace OnlineShop.API.Commands.Users.Update;

public record UpdateUserCommand(UpdateUserDTO UpdateUserDTO):IRequest;
