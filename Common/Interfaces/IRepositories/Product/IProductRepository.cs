
namespace Common.Interfaces.IRepositories.Product;

public interface IProductRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    
}