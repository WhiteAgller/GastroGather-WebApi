using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces.IRepositories.Product;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using Product.Features.Product.Dtos;

namespace Product.Features.Product;

public class GetProductsQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedList<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository<Domain.Product> _repository;

    public GetProductsQueryHandler(IMapper mapper, IProductRepository<Domain.Product> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.PageNumber, request.PageSize)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
