using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.UserDtos;

namespace BlogApp.Business.Dtos.CommentDtos
{
    public record CommentListItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public AuthorDto AppUser { get; set; }
        public IEnumerable<CommentListItemDto> Children { get; set; }
    }
}
