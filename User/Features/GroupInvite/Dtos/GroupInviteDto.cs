using AutoMapper;
using Common.Mappings;

namespace User.Features.GroupInvite.Dtos;

public class GroupInviteDto : IMapFrom<Domain.GroupInvite>
{
    public int Id { get; set; }
    
    public string CreatedBy { get; set; }
    
    public string FriendsUsername { get; set; }
    
    public int GroupId { get; set; }
    
    public bool InvitationAccepted { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.GroupInvite, GroupInviteDto>().ReverseMap();
        }
    }
}