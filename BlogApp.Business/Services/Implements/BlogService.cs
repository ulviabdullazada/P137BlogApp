using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.UserExceptios;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public BlogService(IBlogRepository repo,
            IHttpContextAccessor context,
            IMapper mapper,
            ICategoryRepository categoryRepo,
            UserManager<AppUser> userManager)
        {
            _repo = repo;
            _context = context;
            userId = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _userManager = userManager;
        }

        public async Task CreateAsync(BlogCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
            if (!await _userManager.Users.AnyAsync(u => u.Id == userId)) throw new UserNotFoundException();
            List<BlogCategory> blogCats = new();
            Blog blog = _mapper.Map<Blog>(dto);
            foreach (var id in dto.CategoryIds)
            {
                var cat = await _categoryRepo.FindByIdAsync(id);
                if (cat == null) throw new CategoryNotFoundException();
                blogCats.Add(new BlogCategory { Category = cat, Blog = blog });
            }
            blog.AppUserId = userId;
            blog.BlogCategories = blogCats;
            await _repo.CreateAsync(blog);
            await _repo.SaveAsync();
        }

        public async Task<IEnumerable<BlogListItemDto>> GetAllAsync()
        {
            //1ci usul
            var dto = new List<BlogListItemDto>();
            var entity = _repo.GetAll("AppUser", "BlogCategories", "BlogCategories.Category");
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
                dto.Add(dtoItem);
            }

            return dto;

            //2ci usul
            //var entity = _repo.GetAll("AppUser", "BlogCategories", "BlogCategories.Category");
            //return _mapper.Map<IEnumerable<BlogListItemDto>>(entity);
        }

        public Task<BlogDetailDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, BlogUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
