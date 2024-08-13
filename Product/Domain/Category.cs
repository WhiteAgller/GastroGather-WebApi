using Common.BaseEntity;

namespace Product.Domain;

public class Category : BaseEntity<int>
{
    public string Name { get; set; } = null!;

    public List<Product> Products { get; set; }
}