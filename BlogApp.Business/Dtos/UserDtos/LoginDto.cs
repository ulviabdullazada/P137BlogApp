using FluentValidation;

namespace BlogApp.Business.Dtos.UserDtos;

public record LoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(u => u.UserName)
           .NotEmpty()
           .NotNull()
           .MinimumLength(3)
           .MaximumLength(45);
        RuleFor(u => u.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6);
    }
}
