using MediatR;
using OnlineShop.API.Features;
using OnlineShop.API.ViewModel;

namespace OnlineShop.API.Queries.User.GetByName;

public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, PaginationResult<UserViewModel>>
{
    private readonly IUserService _service;

    public GetUserByNameQueryHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<PaginationResult<UserViewModel>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetUsersByFilterAsync(
            request.FirstName,
            request.LastName,
            request.NationalCode,
            request.OrderType,
            request.PageSize,
            request.PageNumber,
            cancellationToken);
    }
}
