using BlogApp.Core.Enums;

namespace BlogApp.Core.Entities
{
    public class BlogLike:BaseEntity
    {
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Reactions Reaction { get; set; }
    }
}
