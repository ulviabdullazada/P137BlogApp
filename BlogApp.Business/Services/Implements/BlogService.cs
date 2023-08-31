using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.UserExceptios;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Enums;
using BlogApp.DAL.Contexts;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Business.Services.Implements
{
    public class BlogService : IBlogService
    {
        readonly IBlogRepository _repo;
        readonly IHttpContextAccessor _context;
        readonly IMapper _mapper;
        readonly string? userId;
        readonly ICategoryRepository _categoryRepo;
        readonly UserManager<AppUser> _userManager;
        readonly IBlogLikeRepository _blogLikeRepo;
        public BlogService(IBlogRepository repo,
            IHttpContextAccessor context,
            IMapper mapper,
            ICategoryRepository categoryRepo,
            UserManager<AppUser> userManager,
            IBlogLikeRepository blogLikeRepo)
        {
            _repo = repo;
            _context = context;
            userId = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _userManager = userManager;
            _blogLikeRepo = blogLikeRepo;
        }

        public async Task CreateAsync(BlogCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
            if (!await _userManager.Users.AnyAsync(u => u.Id == userId)) throw new UserNotFoundException();
            List<BlogCategory> blogCats = new();
            foreach (var id in dto.CategoryIds)
            {
                //var cat = await _categoryRepo.FindByIdAsync(id);
                //if (cat == null) throw new CategoryNotFoundException();

                if (!await _categoryRepo.IsExistAsync(c=> c.Id == id && !c.IsDeleted)) throw new CategoryNotFoundException();
                blogCats.Add(new BlogCategory { CategoryId = id });
            }
            Blog blog = _mapper.Map<Blog>(dto);
            blog.AppUserId = userId;
            blog.BlogCategories = blogCats;
            await _repo.CreateAsync(blog);
            await _repo.SaveAsync();
        }

        public async Task<IEnumerable<BlogListItemDto>> GetAllAsync()
        {
            //1ci usul
            var dto = new List<BlogListItemDto>();
            var entity = _repo.GetAll("AppUser", "BlogCategories", "BlogCategories.Category","Comments","Comments.Children","Comments.AppUser","BlogLikes");
            List<Category> categories = new();

            foreach (var item in entity)
            {
                categories.Clear();
                foreach (var category in item.BlogCategories)
                {
                    categories.Add(category.Category);
                }
                var dtoItem = _mapper.Map<BlogListItemDto>(item);
                dtoItem.Categories = _mapper.Map<IEnumerable<CategoryListItemDto>>(categories);
                dtoItem.ReactCount = item.BlogLikes.Count;
                dto.Add(dtoItem);
            }
            return dto;

            //2ci usul
            //var entity = _repo.GetAll("AppUser", "BlogCategories", "BlogCategories.Category");
            //return _mapper.Map<IEnumerable<BlogListItemDto>>(entity);
        }

        public async Task<BlogDetailDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var entity = await _repo.FindByIdAsync(id,"AppUser", "BlogCategories", "BlogCategories.Category", "Comments", "Comments.Children", "Comments.AppUser","BlogLikes","BlogLikes.AppUser");
            if (entity == null) throw new NotFoundException<Blog>();
            entity.ViewerCount++;
            await _repo.SaveAsync();
            //var dto = _mapper.Map<BlogDetailDto>(entity);
            return _mapper.Map<BlogDetailDto>(entity);
        }

        public async Task ReactAsync(int id, Reactions reaction)
        {
            await _checkValidate(id);
            var blog = await _repo.FindByIdAsync(id, "BlogLikes");
            if (!blog.BlogLikes.Any(bl => bl.AppUserId == userId && bl.BlogId == id))
            {
                blog.BlogLikes.Add(new BlogLike{BlogId=id, AppUserId=userId, Reaction = reaction });
            }
            else
            {
                var currentReaction = blog.BlogLikes.FirstOrDefault(bl => bl.AppUserId == userId && bl.BlogId == id);
                if (currentReaction == null) throw new NotFoundException<BlogLike>();
                currentReaction.Reaction = reaction;
            }
            await _repo.SaveAsync();
        }
        public async Task RemoveReactAsync(int id)
        {
            await _checkValidate(id);
            var entity =await _blogLikeRepo.GetSingleAsync(bl => bl.AppUserId == userId && bl.BlogId == id);
            if (entity == null) throw new NotFoundException<BlogLike>();
            _blogLikeRepo.Delete(entity);
            await _repo.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _checkValidate(id);
            var entity = await _repo.FindByIdAsync(id);
            if (entity == null) throw new NotFoundException<Blog>();
            if (entity.AppUserId != userId) throw new UserHasNoAccessException();
            _repo.SoftDelete(entity);
            await _repo.SaveAsync();
        }
        public Task UpdateAsync(int id, BlogUpdateDto dto)
        {
            throw new NotImplementedException();
        }
        async Task _checkValidate(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
            if (!await _userManager.Users.AnyAsync(u => u.Id == userId)) throw new UserNotFoundException();
        }
    }
}
