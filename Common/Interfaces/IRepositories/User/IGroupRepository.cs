namespace Common.Interfaces.IRepositories.User;

public interface IGroupRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
}