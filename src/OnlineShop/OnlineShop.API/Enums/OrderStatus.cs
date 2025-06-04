using OnlineShop.API.Attributes;

namespace OnlineShop.API.Enums;
[EnumEndpoint("OrderStatus", "#FF5733")]
public enum OrderStatus
{
    [Info("fa", "در انتظار تأیید")]
    [Info("color", "#FF0000")]
    Pending,

    [Info("fa", "تأیید شده")]
    [Info("color", "#00FF00")]
    Approved,

    [Info("fa", "رد شده")]
    [Info("color", "#0000FF")]
    Rejected
}

