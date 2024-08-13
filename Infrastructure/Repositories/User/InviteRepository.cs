using Common.Interfaces.IRepositories.User;

namespace Infrastructure.Repositories.User;

public class InviteRepository<TEntity> : BaseRepository<TEntity>, IInviteRepository<TEntity>
    where TEntity : class
{
    public InviteRepository(ApplicationDbContext context) : base(context)
    {
    }
    
}