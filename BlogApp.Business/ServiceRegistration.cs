using BlogApp.Business.ExternalServices.Implements;
using BlogApp.Business.ExternalServices.Interfaces;
using BlogApp.Business.Services.Implements;
using BlogApp.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Business;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddHttpContextAccessor();
    }
}
