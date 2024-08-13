using AutoMapper;
using Common.Mappings;

namespace User.Features.FriendInvite.Dtos;

public class FriendInviteDto : IMapFrom<User.Domain.Invite>
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    
    public string FriendUnique { get; set; }
    
    public bool InvitationAccepted { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User.Domain.FriendInvite, FriendInviteDto>().ReverseMap();
        }
    }
}