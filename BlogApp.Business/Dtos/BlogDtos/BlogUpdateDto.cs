using FluentValidation;

namespace BlogApp.Business.Dtos.BlogDtos;

public record BlogUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? CoverImageUrl { get; set; }
}
public class BlogUpdateDtoValidator:AbstractValidator<BlogUpdateDto>
{
    public BlogUpdateDtoValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255);
        RuleFor(b => b.Description)
            .NotEmpty()
            .NotNull();
    }
}
