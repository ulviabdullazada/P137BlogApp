namespace BlogApp.Core.Entities
{
    public class BlogCategory:BaseEntity
    {
        public int BlogId { get; set; }
        public int CategoryId { get; set; }
        public Blog Blog { get; set; }
        public Category Category { get; set; }
    }
}
