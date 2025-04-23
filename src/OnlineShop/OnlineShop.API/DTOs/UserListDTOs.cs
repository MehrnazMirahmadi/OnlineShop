namespace OnlineShop.API.DTOs
{
    public class UserListDTOs
    {
        public string? FilterName { get; set; }
        public List<User> users { get; set; }
    }
}
