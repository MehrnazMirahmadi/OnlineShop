using MediatR;
using OnlineShop.API.ViewModel;

namespace OnlineShop.API.Queries.User.GetByName;
public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, List<UserViewModel>>
{
    private readonly IUserService _service;

    public GetUserByNameQueryHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<List<UserViewModel>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetUserByNameAsync(request.username, cancellationToken);
    }
}

