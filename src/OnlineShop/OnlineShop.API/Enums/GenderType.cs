using OnlineShop.API.Attributes;
using System.ComponentModel;

namespace OnlineShop.API.Enums;
[EnumEndpoint("Genders", "#FF5733")]
public enum GenderType
{
    [Description("مرد")]
    Male = 1,
    [Description("ٌزن")]
    Female = 2,
    [Description("سایر")]
    Other =3,

}
