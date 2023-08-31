using AutoMapper;
using BlogApp.Business.Dtos.CommentDtos;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.UserExceptios;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Business.Services.Implements;

public class CommentService : ICommentService
{
    readonly ICommentRepository _repo;
    readonly IMapper _mapper;
    readonly UserManager<AppUser> _userManager;
    readonly string userId;
    readonly IBlogRepository _blogRepository;
    readonly IHttpContextAccessor _contextAccessor;

    public CommentService(ICommentRepository repo, IMapper mapper, UserManager<AppUser> userManager, IBlogRepository blogRepository, IHttpContextAccessor contextAccessor)
    {
        _repo = repo;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _blogRepository = blogRepository;
        userId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public async Task CreateAsync(int id, CommentCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
        if (!await _userManager.Users.AnyAsync(u => u.Id == userId)) throw new UserNotFoundException();
        if (id <= 0) throw new NegativeIdException();
        if (!await _blogRepository.IsExistAsync(b => b.Id == id)) throw new NotFoundException<Blog>();
        var comment = _mapper.Map<Comment>(dto);
        comment.AppUserId = userId;
        comment.BlogId = id;
        await _repo.CreateAsync(comment);
        await _repo.SaveAsync();
    }

    public Task<IEnumerable<CommentListItemDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
