using System.ComponentModel.DataAnnotations.Schema;
using Common;
using Common.Interfaces.IEntities;

namespace Order.Domain;

public class Order : AuditableEntity, IOrder
{
    public string UserId { get; set; }
    public int TableId { get; set; }
    public ITable Table { get; set; } = null!;

    [ForeignKey(nameof(UserId))] 
    public IUser User { get; set; } = null!;
    
    public decimal TotalCost { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}