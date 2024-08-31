using System.Linq.Expressions;
using Common.Exceptions;
using Common.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(TEntity), id);
    }

    public async Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        var propertyInfo = typeof(TEntity).GetProperty("Id");
        return propertyInfo != null ? (int)propertyInfo.GetValue(entity) : 0;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(TEntity), id);
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<TEntity> GetAll(int pageNumber, int pageSize)
    {
        return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    public IQueryable<TEntity> GetAllByUserName(string username, int pageNumber, int pageSize)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, "CreatedBy");
        
        var usernameValue = Expression.Constant(username, typeof(string));
        var equality = Expression.Equal(property, usernameValue);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(equality, parameter);

        return _dbSet
            .Where(lambda)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

}
