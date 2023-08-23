using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Business.Services.Implements;

public class CategoryService : ICategoryService
{
    readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task CreateAsync(CategoryCreateDto dto)
    {
        Category cat = new Category
        {
            Name = dto.Name,
            LogoUrl = "123",
            IsDeleted = false
        };
        await _repo.CreateAsync(cat);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _repo.GetAll().ToListAsync();

    public async Task<Category> GetByIdAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new CategoryNotFoundException();
        return entity;
    }
}
