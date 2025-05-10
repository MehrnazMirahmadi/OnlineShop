namespace OnlineShop.API.Exceptions;

public class TooManyReuqestException(string message) : Exception(message)
{
}
