using Common.Interfaces.IRepositories.User;


namespace Infrastructure.Repositories.User;

public class FriendInviteRepository<TEntity> : BaseRepository<TEntity>, IFriendInviteRepository<TEntity>
    where TEntity : class
{
    public FriendInviteRepository(ApplicationDbContext context) : base(context)
    {
    }
}