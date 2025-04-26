namespace OnlineShop.API.Validators
{
    public class UpdateUserValidator:AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("نام الزامی است.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("نام خانوادگی الزامی است.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره موبایل الزامی است.")
                .Matches(@"^09\d{9}$").WithMessage("شماره موبایل نامعتبر است.");
        }
    }
}
