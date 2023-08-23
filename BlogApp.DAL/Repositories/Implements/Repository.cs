using BlogApp.Core.Entities.Commons;
using BlogApp.DAL.Contexts;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.DAL.Repositories.Implements;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{
    readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task CreateAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
    }

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
    {
        return Table.Where(expression);
    }

    public async Task<TEntity> FindByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public IQueryable<TEntity> GetAll()
    {
        return Table.AsQueryable();
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Table.SingleOrDefaultAsync(expression);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
