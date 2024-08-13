namespace Common.Interfaces.IEntities;

public interface IUser
{
    public List<IOrder> Orders { get; set; }
}