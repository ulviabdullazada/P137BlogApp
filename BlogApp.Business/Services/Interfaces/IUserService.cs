using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Services.Interfaces;

public interface IUserService
{
    Task RegisterAsync(RegisterDto dto);
    Task<TokenResponseDto> LoginAsync(LoginDto dto);
    Task<ICollection<UserWithRoles>> GetAllAsync();
    Task AddRole(string roleName, string userName);
    Task RemoveRole(string roleName, string userName);
}
