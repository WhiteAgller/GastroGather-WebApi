using System.ComponentModel.DataAnnotations.Schema;
using Common;
using Common.Interfaces.IEntities;

namespace Product.Domain;

public class Product : AuditableEntity, IHasDomainEvent, IProduct
{
    public string Name { get; set; } = null!;
    
    public string? UserId { get; set; } = null!;
    public int CategoryId { get; set; }
    
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;
    
    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();

    public IEnumerable<IOrderItem> OrderItems { get; set; }
}