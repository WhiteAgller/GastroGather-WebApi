using Common.Exceptions;
using Common.Interfaces.IRepositories.User;

namespace Infrastructure.Repositories.User;

public class GroupRepository<TEntity> : BaseRepository<TEntity>, IGroupRepository<TEntity>
    where TEntity : class
{
    public GroupRepository(ApplicationDbContext context) : base(context)
    {
    }

    public new async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(TEntity), id);
    }
    
}