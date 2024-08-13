using Common.Interfaces.IEntities;

namespace Common.Interfaces.IDtos;

public interface ITableDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public int PlaceId { get; set; }
    IEnumerable<IGroup> Groups { get; set; }
}