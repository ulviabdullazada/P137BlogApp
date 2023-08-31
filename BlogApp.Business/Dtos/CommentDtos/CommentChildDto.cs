using BlogApp.Business.Dtos.UserDtos;

namespace BlogApp.Business.Dtos.CommentDtos
{
    public record CommentChildDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public AuthorDto AppUser { get; set; }
    }
}
