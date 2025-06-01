using MediatR;

namespace OnlineShop.API.Commands.Users.Delete;

public record DeleteCommandUser(int id):IRequest;

