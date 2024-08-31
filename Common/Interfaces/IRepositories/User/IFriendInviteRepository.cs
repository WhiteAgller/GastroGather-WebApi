namespace Common.Interfaces.IRepositories.User;

public interface IFriendInviteRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken);
} 