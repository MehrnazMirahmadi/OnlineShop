using OnlineShop.API.Attributes;
using OnlineShop.API.Features;
using System.ComponentModel;
using System.Reflection;

public static class EnumHelper
{
    //public static List<EnumViewModel> ToList(Type enumType)
    //{
    //    return Enum.GetValues(enumType)
    //        .Cast<Enum>()
    //        .Select(e => new EnumViewModel
    //        {
    //            Value = Convert.ToInt32(e),
    //            Name = e.ToString(),
    //            Description = GetEnumDescription(e)
    //        }).ToList();
    //}
    public static List<EnumViewModel> ToViewModel(this IEnumerable<Enum> values)
    {
        return values.Select(e =>
        {
            var type = e.GetType();
            var member = type.GetMember(e.ToString()).FirstOrDefault();
            var infoAttributes = member?.GetCustomAttributes<InfoAttribute>(false) ?? Array.Empty<InfoAttribute>();

            var infoDict = infoAttributes.ToDictionary(attr => attr.Key, attr => attr.Value);

            return new EnumViewModel
            {
                Value = Convert.ToInt32(e),
                Name = e.ToString(),
                Info = infoDict
            };
        }).ToList();
    }
    public static Dictionary<string, object> GetEnumInfo(Enum value)
    {
        var type = value.GetType();
        var memInfo = type.GetMember(value.ToString());
        if (memInfo.Length == 0)
            return new Dictionary<string, object>();

        var attributes = memInfo[0].GetCustomAttributes(typeof(InfoAttribute), false) as InfoAttribute[];

        return attributes?.ToDictionary(attr => attr.Key, attr => attr.Value)
               ?? new Dictionary<string, object>();
    }
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                         .FirstOrDefault() as DescriptionAttribute;

        return attr?.Description ?? value.ToString();
    }
}

