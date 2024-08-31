using Common;
using Common.Interfaces.IEntities;

namespace User.Domain;

public class Group : AuditableEntity, IGroup
{
    public string Name { get; set; } = null!;
    public string AdminUserName { get; set; } = null!;
    
    public List<GroupInvite> GroupInvites { get; set; } = new List<GroupInvite>();
    public IEnumerable<ITable> Tables { get; set; }
    
    //public List<DomainEvent> DomainEvents { get; }
}