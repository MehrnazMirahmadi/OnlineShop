namespace OnlineShop.API.DTOs;

public class UpdateUserDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
