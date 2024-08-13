namespace Common.Interfaces.IEntities;

public interface IInvite
{
    public string UserId { get; set; }
    public int GroupId { get; set; }
    public bool InvitationAccepted { get; set; }
}