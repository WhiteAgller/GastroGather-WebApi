namespace Common.Interfaces.IRepositories.User;

public interface IInviteRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
}