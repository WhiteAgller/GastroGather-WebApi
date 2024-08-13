using Common;
using Common.Interfaces.IEntities;

namespace User.Domain;

public class Group : AuditableEntity, IGroup
{
    public string Name { get; set; } = null!;
    public int MaxNumberOfPeople { get; set; } 
    public String AdminUserId { get; set; } = null!;
    public List<Invite> Invites { get; set; } = new List<Invite>();
    public IEnumerable<ITable> Tables { get; set; }
    
    //public List<DomainEvent> DomainEvents { get; }
}