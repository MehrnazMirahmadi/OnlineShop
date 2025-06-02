using AutoMapper;
using OnlineShop.API.ViewModel;

namespace OnlineShop.API.Helpers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>();
        CreateMap<User, UserDTO>();
    }
}
