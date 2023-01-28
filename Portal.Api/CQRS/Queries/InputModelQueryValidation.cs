namespace Portal.Api.CQRS.Queries;

public class InputModelQueryValidation : AbstractValidator<InputModelQuery>
{
    public InputModelQueryValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(30);

        RuleFor(x => x.Family)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(30);

        RuleFor(x => x.Email)
            .NotEmpty()
            .MinimumLength(7)
            .MaximumLength(60)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
    }
}
