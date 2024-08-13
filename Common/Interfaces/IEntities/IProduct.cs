namespace Common.Interfaces.IEntities;

public interface IProduct
{
    IEnumerable<IOrderItem> OrderItems { get; set; }
}