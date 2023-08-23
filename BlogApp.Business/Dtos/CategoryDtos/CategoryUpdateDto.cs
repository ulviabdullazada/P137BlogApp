using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.Dtos.CategoryDtos;

public record CategoryUpdateDto
{
    public string Name { get; set; }
    public IFormFile? Logo { get; set; }
}
public class CategoryUpdateDtoValidator:AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage("Kateqoriya adı boş ola bilməz")
            .NotNull()
                .WithMessage("Kateqoriya adı null ola bilməz")
            .MaximumLength(64)
                .WithMessage("Kateqoriya adı 64-dən uzun ola bilməz");
    }
}