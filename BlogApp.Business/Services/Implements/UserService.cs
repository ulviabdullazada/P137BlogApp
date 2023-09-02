using AutoMapper;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.Role;
using BlogApp.Business.Exceptions.UserExceptios;
using BlogApp.Business.ExternalServices.Interfaces;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.Business.Services.Implements;

public class UserService : IUserService
{
    readonly UserManager<AppUser> _userManager;
    readonly IMapper _mapper;
    readonly ITokenService _tokenService;
    readonly RoleManager<IdentityRole> _roleManager;
    public UserService(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }

    public async Task AddRole(string roleName, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new NotFoundException<AppUser>();
        if (!await _roleManager.RoleExistsAsync(roleName)) throw new NotFoundException<IdentityRole>();
        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
        {
            string a = "";
            foreach (var err in result.Errors)
            {
                a += err.Description + " ";
            }
            throw new RoleCreateFailedException(a);
        }
    }

    public async Task<ICollection<UserWithRoles>> GetAllAsync()
    {
        ICollection<UserWithRoles> users = new List<UserWithRoles>();
        foreach (var user in await _userManager.Users.ToListAsync())
        {
            users.Add(new UserWithRoles
            {
                User = _mapper.Map<AuthorDto>(user),
                Roles = await _userManager.GetRolesAsync(user)
            });
        }
        return users;
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null) throw new LoginFailedException("Username or password is wrong");
        var result =  await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!result) throw new LoginFailedException("Username or password is wrong");
        return _tokenService.CreateToken(user);
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var user = _mapper.Map<AppUser>(dto);
        if (await _userManager.Users.AnyAsync(u=>dto.UserName == u.UserName || dto.Email == u.Email))
            throw new UserExistException();
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) 
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in result.Errors)
            {
                sb.Append(item.Description + " ");
            }
            throw new RegisterFailedException(sb.ToString().TrimEnd());
        }
    }

    public Task RemoveRole(string roleName, string userName)
    {
        throw new NotImplementedException();
    }
}
