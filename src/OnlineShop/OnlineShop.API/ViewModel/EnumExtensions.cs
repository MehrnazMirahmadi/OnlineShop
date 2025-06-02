using OnlineShop.API.Features;
using System.ComponentModel;

public static class EnumHelper
{
    public static List<EnumViewModel> ToList(Type enumType)
    {
        return Enum.GetValues(enumType)
            .Cast<Enum>()
            .Select(e => new EnumViewModel
            {
                Value = Convert.ToInt32(e),
                Name = e.ToString(),
                Description = GetEnumDescription(e)
            }).ToList();
    }

    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                         .FirstOrDefault() as DescriptionAttribute;

        return attr?.Description ?? value.ToString();
    }
}

