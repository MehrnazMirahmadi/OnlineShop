using MediatR;

namespace OnlineShop.API.Queries.User.GetByName;

public record GetUserByNameQuery(string username) : IRequest<List<UserDTO>>;

