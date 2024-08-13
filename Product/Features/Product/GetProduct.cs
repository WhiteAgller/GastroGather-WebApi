using AutoMapper;
using Common.Interfaces.IRepositories.Product;
using MediatR;
using Product.Features.Product.Dtos;

namespace Product.Features.Product;


public class GetProductQuery : IRequest<ProductDto>
{
    public int Id { get; set; }
}

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository<Domain.Product> _repository;
    
    public GetProductQueryHandler(IMapper mapper, IProductRepository<Domain.Product> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<Domain.Product, ProductDto>((Domain.Product)entity);
    }
}
