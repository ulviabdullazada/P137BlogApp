using Microsoft.AspNetCore.Identity;

namespace BlogApp.Core.Entities;

public class AppUser:IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? ImageUrl { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresDate { get; set; }
    public IEnumerable<Blog> Blogs { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<BlogLike> BlogLikes { get; set; }
}
