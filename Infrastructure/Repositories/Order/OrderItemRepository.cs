using Common.Interfaces.IRepositories.Order;

namespace Infrastructure.Repositories.Order;

public class OrderItemRepository<TEntity> : BaseRepository<TEntity>, IOrderItemRepository<TEntity>
    where TEntity : class
{
    public OrderItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<TEntity> GetAllOrderItemsByOrderId(int orderId, int pageNumber, int pageSize)
    {
        return (IQueryable<TEntity>)_dbSet
            .OfType<global::Order.Domain.OrderItem>()
            .Where(x => x.Order.Id == orderId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}