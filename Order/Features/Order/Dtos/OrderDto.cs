using AutoMapper;
using Common.Interfaces.IDtos;
using Common.Mappings;

namespace Order.Features.Order.Dtos;

public class OrderDto : IMapFrom<Domain.Order>, IOrderDto
{
    public int Id { get; set; }
    
    public int TableId { get; set; }
    
    public string UserId { get; set; }
    
    public List<IOrderItemDto> OrderItems { get; set; } = null!;
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Order, OrderDto>();
        }
    }
}