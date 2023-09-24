using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.ExternalServices.Interfaces;

public interface ITokenService
{
    TokenResponseDto CreateToken(AppUser user, int expires = 60);
    string CreateRefreshToken();
}
