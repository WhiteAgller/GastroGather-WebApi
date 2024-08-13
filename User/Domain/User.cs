using Common;
using Common.Interfaces.IEntities;
using Microsoft.AspNetCore.Identity;

namespace User.Domain;

public class User : IdentityUser, IUser
{
    public List<IOrder> Orders { get; set; } = new List<IOrder>();
    public string Name { get; set; } = null!;
    public List<Invite> Invites = new List<Invite>();
    public List<FriendInvite> FriendInvites = new List<FriendInvite>();
    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}