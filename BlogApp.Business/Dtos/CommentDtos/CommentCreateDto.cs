using FluentValidation;

namespace BlogApp.Business.Dtos.CommentDtos;

public record CommentCreateDto
{
    public string Text { get; set; }
    public int? ParentId { get; set; }
}
public class CommentCreateDtoValidator:AbstractValidator<CommentCreateDto>
{
    public CommentCreateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotNull()
            .NotEmpty();
    }
}

