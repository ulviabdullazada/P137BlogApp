using FluentValidation;

namespace BlogApp.Business.Validators;

public class NotNullOrEmptyValidator : AbstractValidator<string>
{
    public NotNullOrEmptyValidator()
    {
        RuleFor(s => s)
            .NotEmpty()
                .WithMessage("Base validator message")
            .NotNull()
                .WithMessage("Base validator message");
    }
}
