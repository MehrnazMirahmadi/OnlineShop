using OnlineShop.API.Features;
using System.ComponentModel;

namespace OnlineShop.API.ViewModel;

public static class EnumExtensions
{
    public static IEnumerable<EnumViewModel> ToViewModel<TEnum>(this IEnumerable<TEnum> enumValues) where TEnum : Enum
    {
        return enumValues.Select(enumValue => new EnumViewModel
        {
            Value = Convert.ToInt32(enumValue),
            Name = enumValue.ToString(),
            Description = GetDescription(enumValue)
        });
    }

    private static string GetDescription<TEnum>(TEnum enumValue) where TEnum : Enum
    {
        var field = typeof(TEnum).GetField(enumValue.ToString());

        var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                              .FirstOrDefault() as DescriptionAttribute;

        return attribute?.Description ?? enumValue.ToString();
    }
}



