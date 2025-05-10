using System.ComponentModel.DataAnnotations;

namespace OnlineShop.API.Models;

public class User
{
    public int Id { get; set; }


    public string FirstName { get; set; } = string.Empty;

   
    [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی نامعتبر است.")]
    public string NationalCode { get; set; } = string.Empty;

 
    public string LastName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

 
    [RegularExpression(@"^(?:\+98|0098|0)?9\d{9}$", ErrorMessage = "شماره تلفن همراه نامعتبر است.")]
    public string PhoneNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
