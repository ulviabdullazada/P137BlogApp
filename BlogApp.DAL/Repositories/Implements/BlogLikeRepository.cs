using BlogApp.Core.Entities;
using BlogApp.DAL.Contexts;
using BlogApp.DAL.Repositories.Interfaces;

namespace BlogApp.DAL.Repositories.Implements;

public class BlogLikeRepository : Repository<BlogLike>, IBlogLikeRepository
{
    public BlogLikeRepository(AppDbContext context) : base(context)
    {
    }
}
