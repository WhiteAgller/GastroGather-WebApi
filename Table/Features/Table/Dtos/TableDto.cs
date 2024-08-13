using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Common.Interfaces.IDtos;
using Common.Interfaces.IEntities;

namespace Table.Features.Table.Dtos;

public class TableDto : ITableDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public DateTime StartTime { get; set; }
    public int PlaceId { get; set; }
    public IEnumerable<IGroup> Groups { get; set; }

    [ForeignKey(nameof(PlaceId))] 
    public Domain.Place Place { get; set; } = null!;
    
    
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Table, TableDto>()
                .ReverseMap();
        }
    }
}