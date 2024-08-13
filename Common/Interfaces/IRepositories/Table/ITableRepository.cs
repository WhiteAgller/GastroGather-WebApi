namespace Common.Interfaces.IRepositories.Table;

public interface ITableRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    public IQueryable<TEntity> GetAllTablesByGroupId(int groupId, int pageNumber, int pageSize);

    public IQueryable<TEntity> GetAllTablesByUserName(string username, int pageNumber, int pageSize);
}