using MediatR;

namespace OnlineShop.API.Commands.Users.Delete;

public class DeleteCommandUserHandler(IUserService service) : IRequestHandler<DeleteCommandUser>
{
    public async Task Handle(DeleteCommandUser request, CancellationToken cancellationToken)
    {
        await service.SoftDeleteUserAsync(request.id, cancellationToken);
    }
}
