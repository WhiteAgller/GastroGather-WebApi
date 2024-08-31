namespace Common.Interfaces.IEntities;

public interface IInvite
{
    public int GroupId { get; set; }
    public bool InvitationAccepted { get; set; }
}