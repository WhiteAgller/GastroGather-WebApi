using AutoMapper;
using Common.Mappings;

namespace User.Features.Invite.Dtos;

public class InviteDto : IMapFrom<User.Domain.Invite>
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    
    public int GroupId { get; set; }
    
    public bool InvitationAccepted { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User.Domain.Invite, InviteDto>().ReverseMap();
        }
    }
}