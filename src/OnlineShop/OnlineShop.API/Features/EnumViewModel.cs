namespace OnlineShop.API.Features;

public class EnumViewModel
{
    public int Value { get; set; }     
    public string Name { get; set; }
    //  public string Description { get; set; } 
    public Dictionary<string, object> Info { get; set; } = new();
}

