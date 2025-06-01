namespace OnlineShop.API.Commands.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.CreateUserDTO.FirstName)
        .NotNull().NotEmpty().WithMessage("FirsName is required")
        .MaximumLength(100).WithMessage("name must not exceed 100 characters");
    }
}
