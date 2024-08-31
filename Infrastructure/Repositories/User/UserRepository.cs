using Common.Interfaces.IRepositories.User;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.User;

public class UserRepository<TEntity> : BaseRepository<TEntity>, IUserRepository<TEntity>
    where TEntity : class
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<TEntity> GetAllFriends(string username, int pageNumber, int pageSize)
    {
        var invitations = _context.FriendInvites
            .Where(x => x.InvitationAccepted && (x.CreatedBy == username || x.FriendsUsername == username))
            .Select(x => x.CreatedBy == username ? x.FriendsUsername : x.CreatedBy); 

        return FilterUsers(invitations, pageNumber, pageSize);
    }
    
    public IQueryable<TEntity> GetAllFriendInvites(string username, int pageNumber, int pageSize)
    {
        var invitations = _context.FriendInvites
            .Where(x => !x.InvitationAccepted && x.FriendsUsername == username)
            .Select(x => x.CreatedBy); 

        return FilterUsers(invitations, pageNumber, pageSize);
    }
    
    public IQueryable<TEntity> GetAllGroupInvites(string username, int pageNumber, int pageSize)
    {
        var invitations = _context.GroupInvites
            .Where(x => !x.InvitationAccepted && x.FriendsUsername == username)
            .Select(x => x.CreatedBy); 

        return FilterUsers(invitations, pageNumber, pageSize);
    }

    private IQueryable<TEntity> FilterUsers(IQueryable<string> invitations, int pageNumber, int pageSize)
    {
        return (IQueryable<TEntity>)_context.Users
            .Where(x => invitations.Contains(x.UserName))
            .Include(x => x.FriendInvitesSent)
            .Include(x => x.FriendInvitesReceived)
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize); 
    }
}