using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryListItemDto>> GetAllAsync();
    Task<CategoryDetailDto> GetByIdAsync(int id);
    Task CreateAsync(CategoryCreateDto dto);
    Task UpdateAsync(int id, CategoryUpdateDto dto);
    Task RemoveAsync(int id);
    string GetRootPath();
}
