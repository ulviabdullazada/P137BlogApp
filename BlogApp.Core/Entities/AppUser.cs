using Microsoft.AspNetCore.Identity;

namespace BlogApp.Core.Entities;

public class AppUser:IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? ImageUrl { get; set; }
    public IEnumerable<Blog> Blogs { get; set; }
}
