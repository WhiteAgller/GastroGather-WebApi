using Common;
using Common.Interfaces.IEntities;
using Microsoft.AspNetCore.Identity;

namespace User.Domain;

public class User : IdentityUser, IUser
{
    public List<IOrder> Orders { get; set; } = new List<IOrder>();
    
    public List<GroupInvite> Invites = new List<GroupInvite>();
    
    public List<FriendInvite> FriendInvitesSent = new List<FriendInvite>();
    
    public List<FriendInvite> FriendInvitesReceived = new List<FriendInvite>();
    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}