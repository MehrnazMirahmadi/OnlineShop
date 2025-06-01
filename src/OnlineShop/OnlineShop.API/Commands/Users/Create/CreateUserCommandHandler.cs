using MediatR;

namespace OnlineShop.API.Commands.Users.Create;

public class CreateUserCommandHandler(IUserService service) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await service.CreateUserAsync(request.CreateUserDTO, cancellationToken);
    }
}
