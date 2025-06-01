using AutoMapper;
using Humanizer;
using OnlineShop.API.Features;
using OnlineShop.API.ViewModel;

namespace OnlineShop.API.Helpers;

public static class MapperHelper
{
    private static readonly IMapper _mapper;

    static MapperHelper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            
        });

        _mapper = config.CreateMapper();
    }

    public static UserViewModel ToViewModel(this User entity)
    {
        return _mapper.Map<UserViewModel>(entity);
    }

    public static List<UserViewModel> ToViewModel(this List<User> entities)
    {
        return _mapper.Map<List<UserViewModel>>(entities);
    }

    public static IEnumerable<EnumViewModel> ToViewModel(this IEnumerable<Enum> enumValues)
    {
        return enumValues.Select(enumValue => new EnumViewModel
        {
            Value = (int)(object)enumValue,
            Name = enumValue.ToString(),
            Description = enumValue.Humanize()
        });
    }
}
