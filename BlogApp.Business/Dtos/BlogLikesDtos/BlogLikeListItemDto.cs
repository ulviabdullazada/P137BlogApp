using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;
using BlogApp.Core.Enums;

namespace BlogApp.Business.Dtos.BlogLikesDtos;

public record BlogLikeListItemDto
{
    public AuthorDto AppUser { get; set; }
    public Reactions Reaction { get; set; }
}
