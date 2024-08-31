using AutoMapper;
using User.Features.FriendInvite.Dtos;

namespace User.Features.User.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public string ConcurrencyStamp { get; set; }
    public List<FriendInviteDto> FriendInvitesSent { get; set; }
    public List<FriendInviteDto> FriendInvitesReceived { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.User, UserDto>()
                .ForMember(x => x.FriendInvitesSent, y => y.MapFrom(z => z.FriendInvitesSent))
                .ForMember(x => x.FriendInvitesReceived, y => y.MapFrom(z => z.FriendInvitesReceived))
                .ReverseMap();
        }
    }
}