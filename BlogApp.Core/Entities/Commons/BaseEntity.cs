namespace BlogApp.Core.Entities.Commons;

public class BaseEntity
{
    public int Id { get; set; }
    public virtual bool IsDeleted { get; set; }
}
