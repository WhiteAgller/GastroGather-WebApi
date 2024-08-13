using Common.Interfaces.IRepositories.Table;
using User.Domain;


namespace Infrastructure.Repositories.Table;

public class TableRepository<TEntity> : BaseRepository<TEntity>, ITableRepository<TEntity>
    where TEntity : class
{
    public TableRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<TEntity> GetAllTablesByGroupId(int groupId, int pageNumber, int pageSize)
    {
        return (IQueryable<TEntity>)_dbSet
            .OfType<global::Table.Domain.Table>()
            .Where(x => x.GroupId == groupId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public IQueryable<TEntity> GetAllTablesByUserName(string username, int pageNumber, int pageSize)
    {
        return (IQueryable<TEntity>)_dbSet
            .OfType<global::Table.Domain.Table>()
            .Where(x => (x.Group as Group)!.Invites
                .Where(y => y.InvitationAccepted)
                .Any(z => z.CreatedBy == username))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}