
namespace Common.Interfaces.IEntities;

public interface IOrderItem
{
    public int ProductId { get; set; }
    public IProduct Product { get; set; }
    
}