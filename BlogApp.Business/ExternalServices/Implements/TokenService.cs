using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Business.ExternalServices.Interfaces;
using BlogApp.Business.Services.Implements;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlogApp.Business.ExternalServices.Implements;

public class TokenService : ITokenService
{
    readonly IConfiguration _configuration;
    readonly UserManager<AppUser> _userManager; //Bu hissə videodan fərqlidi.
    //readonly IRoleService _roleService; -> Köhnə versiya

    public TokenService(IConfiguration configuration, 
        UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
        //byte[] bytes = new byte[32];
        //var random = RandomNumberGenerator.Create();
        //random.GetBytes(bytes);
        //return Convert.ToBase64String(bytes);
    }

    public TokenResponseDto CreateToken(AppUser user, int expires = 60)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname)
        };
        foreach (var userRole in _userManager.GetRolesAsync(user).Result) //Köhnə versiyada bütün rolları gətirirdik. Burda isə sadəcə userin rollarını götürdük
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        //foreach (var userRole in _roleService.GetAllAsync().Result) -> Burda roleServicedəki bütün rolları seçirik
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, userRole.Name));
        //} 
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwtSecurity = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(expires),
            credentials);
        JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        string token = jwtHandler.WriteToken(jwtSecurity);
        string refreshToken = CreateRefreshToken();
        var refreshTokenExpires = jwtSecurity.ValidTo.AddMinutes(expires / 3);
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresDate = refreshTokenExpires;
        _userManager.UpdateAsync(user).Wait();
        return new()
        {
            Token = token,
            Expires = jwtSecurity.ValidTo,
            Username = user.UserName,
            RefreshToken = refreshToken,
            RefreshTokenExpires = refreshTokenExpires
        };
    }
}
