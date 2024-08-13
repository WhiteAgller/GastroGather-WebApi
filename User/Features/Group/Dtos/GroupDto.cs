using AutoMapper;
using Common.Mappings;

namespace User.Features.Group.Dtos;

public class GroupDto : IMapFrom<Domain.Group>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int MaxNumberOfPeople { get; set; }
    public String AdminUserId { get; set; } = null!;
    public List<Domain.User> Users { get; set; } = new List<Domain.User>();
    public List<Domain.Invite> Invites { get; set; } = new List<Domain.Invite>();
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Group, GroupDto>();
        }
    }
}