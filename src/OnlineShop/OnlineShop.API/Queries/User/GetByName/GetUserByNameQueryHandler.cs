using MediatR;

namespace OnlineShop.API.Queries.User.GetByName;
public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, List<UserDTO>>
{
    private readonly IUserService _service;

    public GetUserByNameQueryHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<List<UserDTO>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetUserByNameAsync(request.username, cancellationToken);
    }
}

