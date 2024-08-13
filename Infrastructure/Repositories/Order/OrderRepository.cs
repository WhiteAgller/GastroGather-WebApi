using Common.Interfaces.IRepositories.Order;

namespace Infrastructure.Repositories.Order;

public class OrderRepository<TEntity> : BaseRepository<TEntity>, IOrderRepository<TEntity>
    where TEntity : class
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<TEntity> GetOrdersByTableId(int userId, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}