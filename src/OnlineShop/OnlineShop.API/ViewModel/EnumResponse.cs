namespace OnlineShop.API.ViewModel;

public class EnumResponse<T>
{
    public string Color { get; set; }
    public IEnumerable<T> Values { get; set; }
}

