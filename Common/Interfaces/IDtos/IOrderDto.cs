namespace Common.Interfaces.IDtos;

public interface IOrderDto
{
    public int Id { get; set; }
    
    public int TableId { get; set; }
    
    public string UserId { get; set; }
    
    public List<IOrderItemDto> OrderItems { get; set; }
}