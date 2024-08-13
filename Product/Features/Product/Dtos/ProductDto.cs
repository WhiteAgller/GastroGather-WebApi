using AutoMapper;
using Common.Mappings;

namespace Product.Features.Product.Dtos;

public class ProductDto : IMapFrom<Domain.Product>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Product, ProductDto>();
        }
    }
}