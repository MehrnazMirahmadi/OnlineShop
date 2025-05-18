namespace OnlineShop.API.DTOs;

public class UpdateUserDTO
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public string PhoneNumber { get; set; }
    public required string NationalCode { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
}
