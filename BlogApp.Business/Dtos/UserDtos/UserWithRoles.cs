using Microsoft.AspNetCore.Identity;

namespace BlogApp.Business.Dtos.UserDtos
{
    public record UserWithRoles
    {
        public AuthorDto User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
