using System.ComponentModel.DataAnnotations.Schema;
using Common.BaseEntity;
using Common.Interfaces.IEntities;

namespace User.Domain;

public class FriendInvite : BaseEntity<int>, IFriendInvite
{
    public string UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    public string FriendUnique { get; set; }
    
    [ForeignKey(nameof(FriendUnique))]
    public User Friend { get; set; }
    
    public bool InvitationAccepted { get; set; }
}