using OnlineShop.API.Features;
using OnlineShop.API.ViewModel;

namespace OnlineShop.API.Services
{
    public interface IUserService
    {
        Task<PaginationResult<UserViewModel>> GetUsersByFilterAsync(
      string? firstName,
      string? lastName,
      string? nationalCode,
      OrderType? orderType,
      int pageSize,
      int pageNumber,
      CancellationToken cancellationToken);
        Task CreateUserAsync(CreateUserDTO userDTO, CancellationToken cancellationToken);
        Task UpdateUserAsync(UpdateUserDTO userDTO, CancellationToken cancellationToken);
        Task SoftDeleteUserAsync(int id, CancellationToken cancellationToken);
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
        Task<UserViewModel> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
