
namespace Common.Interfaces.IEntities;

public interface IGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxNumberOfPeople { get; set; } 
    public String AdminUserId { get; set; }
    public IEnumerable<ITable> Tables { get; set; }
}