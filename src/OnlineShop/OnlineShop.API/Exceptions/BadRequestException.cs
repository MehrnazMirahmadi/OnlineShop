namespace OnlineShop.API.Exceptions;

public class BadRequestException : Exception
{
    public Dictionary<string, string[]> Errors { get; init; } = new();

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, Dictionary<string, string[]> errors) : base(message)
    {
        Errors = errors;
    }
}
