using OnlineShop.API.Attributes;
using System.ComponentModel;

namespace OnlineShop.API.Enums;


[EnumEndpoint("TypeBs")]
public enum ModelBType
{
    [Description("نوع اول")]
    Type1 = 1,
    [Description("نوع دوم")]
    Type2 = 2,
    [Description("نوع سوم")]
    Type3 = 3,
    [Description("نوع چهارم")]
    Type4 = 4
}