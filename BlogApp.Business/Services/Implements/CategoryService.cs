using AutoMapper;
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
    readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
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


    public async Task RemoveAsync(int id)
    {
        var entity = await _getCategoryAsync(id);
        _repo.Delete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var entity = await _getCategoryAsync(id);
        //_mapper.Map(dto, entity);
        _mapper.Map<Category>(dto);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<CategoryListItemDto>> GetAllAsync()
    {
        //1. Selectle sechmek
        //return await _repo.GetAll().Select(x => new CategoryListItemDto { Id = x.Id, Name = x.Name, LogoUrl = x.LogoUrl, IsDeleted = x.IsDeleted}).ToListAsync();

        //2. Foreachla bir bir add etmek
        //List<CategoryListItemDto> categories = new List<CategoryListItemDto>();
        //foreach (var item in _repo.GetAll())
        //{
        //    categories.Add(new CategoryListItemDto()
        //    {
        //        Id = item.Id,
        //        IsDeleted=item.IsDeleted,
        //        LogoUrl=item.LogoUrl,
        //        Name = item.Name
        //    });
        //}
        //return categories;

        //3. AutoMapper
        return _mapper.Map<IEnumerable<CategoryListItemDto>>(_repo.GetAll());
    }

    public async Task<CategoryDetailDto> GetByIdAsync(int id)
    {
        var entity = await _getCategoryAsync(id);
        return _mapper.Map<CategoryDetailDto>(entity);
    }
    async Task<Category> _getCategoryAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new CategoryNotFoundException();
        return entity;
    }
}
