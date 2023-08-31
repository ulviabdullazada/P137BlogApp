using AutoMapper;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Dtos.CommentDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Profiles
{
    public class CommentMappingProfile:Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentListItemDto>();
            CreateMap<Comment, CommentDetailDto>();
            CreateMap<CommentChildDto, Comment>().ReverseMap();
            CreateMap<CommentCreateDto, Comment>();
        }
    }
}
