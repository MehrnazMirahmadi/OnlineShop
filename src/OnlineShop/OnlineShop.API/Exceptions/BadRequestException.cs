namespace OnlineShop.API.Exceptions;

public class BadRequestException(string message) : Exception(message)
{
}
