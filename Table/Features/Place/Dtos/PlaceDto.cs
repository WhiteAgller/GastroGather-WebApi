using AutoMapper;
using Common.Mappings;

namespace Table.Features.Place.Dtos;

public class PlaceDto : IMapFrom<Domain.Place>
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Place, PlaceDto>()
                .ReverseMap();
        }
    }
}