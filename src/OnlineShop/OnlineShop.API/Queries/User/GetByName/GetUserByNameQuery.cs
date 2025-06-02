using MediatR;
using OnlineShop.API.Features;
using OnlineShop.API.ViewModel;

namespace OnlineShop.API.Queries.User.GetByName;


public class GetUserByNameQuery : IRequest<PaginationResult<UserViewModel>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }
    public OrderType? OrderType { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}