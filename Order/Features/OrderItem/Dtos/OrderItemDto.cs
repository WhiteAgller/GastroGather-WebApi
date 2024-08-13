using AutoMapper;
using Common.Mappings;

namespace Order.Features.OrderItem.Dtos;

public class OrderItemDto : IMapFrom<Domain.OrderItem>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}