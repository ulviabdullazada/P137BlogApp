using AutoMapper;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Profiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryListItemDto>().ReverseMap();
        CreateMap<Category, CategoryDetailDto>();
        CreateMap<CategoryUpdateDto, Category>();
        CreateMap<CategoryCreateDto, Category>();
    }
}
