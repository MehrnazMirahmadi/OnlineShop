namespace OnlineShop.API.ViewModel
{
    public class UserViewModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public required string NationalCode { get; set; }
        public string FullName { get; set; }
    }
}
