using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces.IRepositories.Product;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using Product.Features.Product.Dtos;

namespace Product.Features.Product;

public class GetProductsByUserIdQuery : IRequest<PaginatedList<ProductDto>>
{
    public string Username { get; set; } = null!;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetProductsByUserIdQueryValidator : AbstractValidator<GetProductsByUserIdQuery>
{
    public GetProductsByUserIdQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required");
    }
}

internal sealed class GetProductsByUserIdQueryQueryHandler : IRequestHandler<GetProductsByUserIdQuery, PaginatedList<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository<Domain.Product> _repository;
    

    public GetProductsByUserIdQueryQueryHandler(IMapper mapper, IProductRepository<Domain.Product> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllByUserName(request.Username, request.PageNumber, request.PageSize)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
