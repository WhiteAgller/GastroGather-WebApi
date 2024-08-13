using AutoMapper;
using Common.Interfaces.IEntities;
using Common.Interfaces.IRepositories.Product;
using Common.Mappings;
using FluentValidation;
using MediatR;
using Product.Features.Product.Dtos;

namespace Product.Features.Product;

public class UpdateProductCommand : UpdateProductRequestBody, IRequest<ProductDto>
{
    public int Id { get; set; }
}

public class UpdateProductRequestBody : IMapFrom<UpdateProductCommand>
{
    public string Name { get; set; } = null!;
    
    public int CategoryId { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateProductRequestBody, UpdateProductCommand>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ReverseMap();
        }
    }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(30);
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}

internal sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository<Domain.Product> _repository;

    public UpdateProductCommandHandler(IMapper mapper, IProductRepository<Domain.Product> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        product.Name = request.Name;
        product.CategoryId = request.CategoryId;
        var newEntity = await _repository.UpdateAsync(product, cancellationToken);
        return _mapper.Map<Domain.Product, ProductDto>(newEntity);
    }
}