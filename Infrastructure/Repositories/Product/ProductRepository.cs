using Common.Interfaces.IRepositories.Product;

namespace Infrastructure.Repositories.Product;

public class ProductRepository<TEntity> : BaseRepository<TEntity>, IProductRepository<TEntity>
    where TEntity : class
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}