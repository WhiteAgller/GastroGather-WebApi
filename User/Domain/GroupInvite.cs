using System.ComponentModel.DataAnnotations.Schema;
using Common.BaseEntity;
using Common.Interfaces.IEntities;

namespace User.Domain;

public class GroupInvite : BaseEntity<int>, IInvite
{
    [ForeignKey(nameof(CreatedBy))]
    public User User { get; set; }
    
    public string FriendsUsername { get; set; }
    
    [ForeignKey(nameof(FriendsUsername))]
    public User Friend { get; set; }
    
    public int GroupId { get; set; }
    
    [ForeignKey(nameof(GroupId))]
    public Group Group { get; set; }
    
    public bool InvitationAccepted { get; set; }
}