using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Interfaces.IEntities;

public interface IOrder
{
    int Id { get; set; }
    
    public string UserId { get; set; }
    
    public int TableId { get; set; }
    
    [ForeignKey(nameof(TableId))]
    public ITable Table { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public IUser User { get; set; }
}