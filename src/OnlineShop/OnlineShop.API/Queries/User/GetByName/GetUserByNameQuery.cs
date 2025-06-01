using MediatR;
using OnlineShop.API.ViewModel;

namespace OnlineShop.API.Queries.User.GetByName;

public record GetUserByNameQuery(string username) : IRequest<List<UserViewModel>>;

