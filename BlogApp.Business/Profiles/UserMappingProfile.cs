using AutoMapper;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Profiles;

public class UserMappingProfile:Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterDto, AppUser>();
        CreateMap<AppUser, AuthorDto>();
    }
}
