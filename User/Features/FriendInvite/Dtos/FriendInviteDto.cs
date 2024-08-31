using AutoMapper;
using Common.Mappings;

namespace User.Features.FriendInvite.Dtos;

public class FriendInviteDto : IMapFrom<Domain.GroupInvite>
{
    public int Id { get; set; }
    
    public string CreatedBy { get; set; }
    
    public string FriendsUsername { get; set; }
    
    public bool InvitationAccepted { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.FriendInvite, FriendInviteDto>().ReverseMap();
        }
    }
}