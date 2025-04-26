using System.ComponentModel.DataAnnotations;

namespace OnlineShop.API.DTOs;

public class CreateUserDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }   
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}


