using MediatR;

namespace OnlineShop.API.Commands.Users.Update;

public class UpdateUserCommandHandler(IUserService service) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await service.UpdateUserAsync(request.UpdateUserDTO, cancellationToken);
    }
}
