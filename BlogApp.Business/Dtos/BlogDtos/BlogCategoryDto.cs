using BlogApp.Business.Dtos.CategoryDtos;

namespace BlogApp.Business.Dtos.BlogDtos
{
    public record BlogCategoryDto
    {
        public CategoryListItemDto Category { get; set; }
    }
}
