namespace Common.Interfaces.IEntities;

public interface ITable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public int PlaceId { get; set; }
    public int GroupId { get; set; }
    public IGroup Group { get; set; }
    public IEnumerable<IOrder> Orders { get; set; }
}