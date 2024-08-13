namespace Common.Interfaces.IRepositories.Order;

public interface IOrderItemRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    public IQueryable<TEntity> GetAllOrderItemsByOrderId(int orderId, int pageNumber, int pageSize);
}