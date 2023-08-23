namespace BlogApp.Business.Exceptions.Category;

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException() : base("Kateqoriya tapılmadı") { }

    public CategoryNotFoundException(string? message) : base(message)
    {
    }
}
