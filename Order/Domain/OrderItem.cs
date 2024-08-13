using Common.BaseEntity;
using Common.Interfaces.IEntities;

namespace Order.Domain;

public class OrderItem : BaseEntity<int>, IOrderItem
{
    public Order Order { get; set; }
    public int OrderId { get; set; }
    public IProduct Product { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
}