namespace Common.Interfaces.IRepositories.User;

public interface IUserRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    IQueryable<TEntity> GetAllFriends(string username, int pageNumber, int pageSize);

    IQueryable<TEntity> GetAllFriendInvites(string username, int pageNumber, int pageSize);
    
    IQueryable<TEntity> GetAllGroupInvites(string username, int pageNumber, int pageSize);
}