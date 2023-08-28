using BlogApp.Business.Dtos.BlogDtos;

namespace BlogApp.Business.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogListItemDto>> GetAllAsync();
        Task<BlogDetailDto> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateDto dto);
        Task UpdateAsync(int id, BlogUpdateDto dto);
        Task RemoveAsync(int id);
    }
}
