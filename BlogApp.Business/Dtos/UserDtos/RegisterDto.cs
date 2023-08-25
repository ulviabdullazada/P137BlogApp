using FluentValidation;
using System.Text.RegularExpressions;

namespace BlogApp.Business.Dtos.UserDtos;

public record RegisterDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(25);
        RuleFor(u => u.Surname)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(30);
        RuleFor(u => u.Email)
            .NotEmpty()
            .NotNull()
            .Must(u =>
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var result = regex.Match(u);
                return result.Success;
            })
                .WithMessage("Please enter valid email");
        RuleFor(u => u.UserName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(45);
        RuleFor(u => u.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6);
        RuleFor(u => u)
            .Must(u => u.Password == u.ConfirmPassword)
                .WithMessage("Password must be equal to ConfirmPassword");
    }
}
