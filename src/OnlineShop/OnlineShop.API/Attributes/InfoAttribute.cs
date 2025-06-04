namespace OnlineShop.API.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class InfoAttribute : Attribute
{
    public string Key { get; }
    public object Value { get; }

    public InfoAttribute(string key, object value)
    {
        Key = key;
        Value = value;
    }
}
