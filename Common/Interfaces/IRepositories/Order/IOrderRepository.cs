namespace Common.Interfaces.IRepositories.Order;

public interface IOrderRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    public IQueryable<TEntity> GetOrdersByTableId(int userId, int pageNumber, int pageSize);
}