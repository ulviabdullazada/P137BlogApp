using BlogApp.Business.Dtos.CommentDtos;

namespace BlogApp.Business.Services.Interfaces;

public interface ICommentService
{
    public Task<IEnumerable<CommentListItemDto>> GetAllAsync();
    public Task CreateAsync(int id, CommentCreateDto dto);
}
