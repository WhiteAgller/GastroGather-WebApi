using System.ComponentModel.DataAnnotations.Schema;
using Common.BaseEntity;
using Common.Interfaces.IEntities;

namespace User.Domain;

public class Invite : BaseEntity<int>, IInvite
{
    public string UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    public int GroupId { get; set; }
    
    [ForeignKey(nameof(GroupId))]
    public Group Group { get; set; }
    
    public bool InvitationAccepted { get; set; }
}