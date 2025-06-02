using AutoMapper;
using Mapster;
using Mehrnaz.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.API.Helpers;
using OnlineShop.API.Proxies;
using OnlineShop.API.Repository;
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
    public async Task<List<UserViewModel>> GetUserByNameAsync(string username, CancellationToken cancellationToken)
    {
       

        var cacheKey = $"users_by_name_{username ?? "all"}";

        if (memoryCache.TryGetValue(cacheKey, out List<UserViewModel> cachedUsers))
        {
            return cachedUsers;
        }

        List<User> users;

        if (string.IsNullOrWhiteSpace(username))
        {
            users = await _userRepository.GetAllUsersAsync(cancellationToken);
        }
        else
        {
            users = await _userRepository.GetUsersByNameAsync(username, cancellationToken);

            if (users == null || users.Count == 0)
            {
                users = await _userRepository.GetAllUsersAsync(cancellationToken);
            }
        }

        //var userViewModels = users.ToViewModel();
        var userViewModels = users.Adapt<List<UserViewModel>>(); // Mapster 



        memoryCache.Set(cacheKey, userViewModels, TimeSpan.FromMinutes(10));

        return userViewModels;
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
