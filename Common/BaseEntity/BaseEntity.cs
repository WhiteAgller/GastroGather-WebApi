namespace Common.BaseEntity;

public partial class BaseEntity<T>
{
    public T Id { get; set; } = default!;
    
    public string CreatedBy { get; set; } = "";
}