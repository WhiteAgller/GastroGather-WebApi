namespace Common.Interfaces.IDtos;

public interface IOrderItemDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
}