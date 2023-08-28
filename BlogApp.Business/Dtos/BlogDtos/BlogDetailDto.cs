using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Dtos.BlogDtos;

public record BlogDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public int ViewerCount { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedTime { get; set; }
    public AuthorDto AppUser { get; set; }
    public IEnumerable<Category> Categories { get; set; }
}
