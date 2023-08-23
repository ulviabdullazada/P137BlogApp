using BlogApp.Core.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
    public DbSet<TEntity> Table { get; }
    public IQueryable<TEntity> GetAll();
    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);
    public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression);
    public Task<TEntity> FindByIdAsync(int id);
    public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression);
    public Task CreateAsync(TEntity entity);
    public Task SaveAsync();
}
