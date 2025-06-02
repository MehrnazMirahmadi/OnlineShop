using AutoMapper;
using Mapster;
using Mehrnaz.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.API.Features;
using OnlineShop.API.Helpers;
using OnlineShop.API.Proxies;
using OnlineShop.API.Repository;
using OnlineShop.API.Specification;
using OnlineShop.API.ViewModel;

namespace OnlineShop.Application.Services;

public class UserService
    (IUserRepository _userRepository
    , IMemoryCache memoryCache
    , ITrackingCodeProxy trackingCodeProxy
    , IMapper _mapper
    ) 
    : IUserService
{
    public async Task<PaginationResult<UserViewModel>> GetUsersByFilterAsync(
      string? firstName,
      string? lastName,
      string? nationalCode,
      OrderType? orderType,
      int pageSize,
      int pageNumber,
      CancellationToken cancellationToken)
    {
        var spec = new GetUsersByFilterSpecification(firstName, lastName, nationalCode, orderType, pageSize, pageNumber);

        var users = await _userRepository.ListAsync(spec, cancellationToken);
        var totalCount = await _userRepository.CountAsync(spec.Criteria, cancellationToken);

        var userViewModels = users.Adapt<List<UserViewModel>>();

        return PaginationResult<UserViewModel>.Create(pageSize, pageNumber, totalCount, userViewModels);
    }




    public async Task CreateUserAsync(CreateUserDTO userDTO, CancellationToken cancellationToken)
    {
        var hashedPassword = userDTO.Password.ToSha256();
        var trackingCode = await trackingCodeProxy.Get(cancellationToken);
        var user = User.Create(
            userDTO.FirstName,
            userDTO.LastName,
            userDTO.NationalCode,
            userDTO.PhoneNumber,
            hashedPassword,
            trackingCode
        );

        await _userRepository.CreateUserAsync(user, cancellationToken);
    }

    public async Task UpdateUserAsync(UpdateUserDTO userDTO, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(userDTO.Id, cancellationToken);
        if (user == null) return;

        user.Update(
            userDTO.FirstName,
            userDTO.LastName,
            userDTO.PhoneNumber,
            userDTO.NationalCode,
            userDTO.IsActive,
            userDTO.IsDelete
        );

        if (!string.IsNullOrWhiteSpace(userDTO.Password))
        {
            user.ChangePassword(userDTO.Password.ToSha256());
        }

        await _userRepository.UpdateUserAsync(user, cancellationToken);
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteUserAsync(id, cancellationToken);
    }

    public async Task<UserViewModel> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);

        return user == null ? null : _mapper.Map<UserViewModel>(user); //  AutoMapper
    }

  



    public async Task SoftDeleteUserAsync(int id, CancellationToken cancellationToken)
    {
       /* var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
        if (user == null) return;

         user.SoftDelete();
        
        await _userRepository.UpdateUserAsync(user, cancellationToken);*/

             await _userRepository.SoftDeleteUserAsync(id, cancellationToken);
    }
}
