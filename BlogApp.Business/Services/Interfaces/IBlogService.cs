using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Core.Enums;

namespace BlogApp.Business.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogListItemDto>> GetAllAsync();
        Task<BlogDetailDto> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateDto dto);
        Task UpdateAsync(int id, BlogUpdateDto dto);
        Task RemoveAsync(int id);
        Task ReactAsync(int id, Reactions reactions);
        Task RemoveReactAsync(int id);
    }
}
