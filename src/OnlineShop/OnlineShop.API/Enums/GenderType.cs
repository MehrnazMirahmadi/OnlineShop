using OnlineShop.API.Attributes;
using System.ComponentModel;

namespace OnlineShop.API.Enums;

[EnumEndpoint("Genders")]
public enum GenderType
{
    [Description("مرد")]
    Male = 1,
    [Description("زن")]
    Female = 2,
}
