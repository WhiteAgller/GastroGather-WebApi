using Common.Interfaces.IRepositories.User;

namespace Infrastructure.Repositories.User;

public class GroupGroupInviteRepository<TEntity> : BaseRepository<TEntity>, IGroupInviteRepository<TEntity>
    where TEntity : class
{
    public GroupGroupInviteRepository(ApplicationDbContext context) : base(context)
    {
    }
    
}