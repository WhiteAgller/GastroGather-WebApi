using System.ComponentModel.DataAnnotations.Schema;
using Common;
using Common.Interfaces.IEntities;

namespace Table.Domain;

public class Table : AuditableEntity, ITable
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public DateTime StartTime { get; set; }
    public int PlaceId { get; set; }

    [ForeignKey(nameof(PlaceId))] 
    public Place Place { get; set; } = null!;
    public int GroupId { get; set; }

    [ForeignKey(nameof(GroupId))] 
    public IGroup Group { get; set; } = null;
    public IEnumerable<IOrder> Orders { get; set; }
}