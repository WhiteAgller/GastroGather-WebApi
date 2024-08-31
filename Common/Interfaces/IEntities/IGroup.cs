
namespace Common.Interfaces.IEntities;

public interface IGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AdminUserName { get; set; }
    public IEnumerable<ITable> Tables { get; set; }
}