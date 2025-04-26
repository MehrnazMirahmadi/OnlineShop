namespace OnlineShop.API.Models;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; } 
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
