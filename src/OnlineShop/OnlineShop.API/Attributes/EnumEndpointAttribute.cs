namespace OnlineShop.API.Attributes;


[AttributeUsage(AttributeTargets.Enum)]
public class EnumEndpointAttribute : Attribute
{
    public string Route { get; }
    public string Color { get; }  // اضافه کردن رنگ

    public EnumEndpointAttribute(string route, string color = "#000000")
    {
        Route = route;
        Color = color;
    }
}

