namespace Common.Interfaces.IRepositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    IQueryable<TEntity> GetAll(int pageNumber, int pageSize);
    IQueryable<TEntity> GetAllByUserName(string username, int pageNumber, int pageSize);
}