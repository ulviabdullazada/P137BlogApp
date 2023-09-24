using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.Validators;

public class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator(int sizewithMb = 3, string contentType = "image")
    {
        RuleFor(f => f.ContentType)
            .Must(t => t.Contains(contentType))
                .WithMessage("File format is wrong");
        RuleFor(f => f.Length)
            .LessThanOrEqualTo(sizewithMb * 1024 * 1024)
                .WithMessage("File max size must be "+ sizewithMb + "mb");
    }
}
