using FluentValidation.Results;
using MediatR;
using OnlineShop.API.Exceptions;

namespace OnlineShop.API.Behaviours;

public class ValidationBehaviour<TRequest, TRseponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TRseponse> where TRequest : notnull
{
    public async Task<TRseponse> Handle(TRequest request, RequestHandlerDelegate<TRseponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var validationResult = await validators.First().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = Serialize(validationResult.Errors);
                throw new BadRequestException("Input is not valid", errors);
            }
        }

        return await next(cancellationToken);
    }
    private static Dictionary<string, string[]> Serialize(List<ValidationFailure> validationFailures)
    {
        var errors = validationFailures
            .GroupBy(validationFailure => validationFailure.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(validationFailure => validationFailure.ErrorMessage).ToArray()
            );

        return errors;
    }
}
