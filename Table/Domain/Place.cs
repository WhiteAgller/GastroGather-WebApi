using Common;
using Common.BaseEntity;

namespace Table.Domain;

public class Place : BaseEntity<int>
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public List<Table> Tables = new List<Table>();
}