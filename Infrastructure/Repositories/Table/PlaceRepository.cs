using Common.Interfaces.IRepositories.Table;

namespace Infrastructure.Repositories.Table;

public class PlaceRepository<TEntity> : BaseRepository<TEntity>, IPlaceRepository<TEntity>
    where TEntity : class
{
    public PlaceRepository(ApplicationDbContext context) : base(context)
    {
    }
}