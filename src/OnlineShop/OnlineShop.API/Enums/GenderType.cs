using OnlineShop.API.Attributes;

namespace OnlineShop.API.Enums;

[EnumEndpoint("Genders", "#FF5733")]
public enum GenderType
{
    [Info("fa", "مرد")]
    [Info("color", "#2196F3")]
    Male = 1,

    [Info("fa", "زن")]
    [Info("color", "#E91E63")]
    Female = 2,

    [Info("fa", "سایر")]
    [Info("color", "#9E9E9E")]
    Other = 3,
}
