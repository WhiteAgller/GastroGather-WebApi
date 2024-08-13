using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces.IDtos;
using Common.Interfaces.IRepositories.Order;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using Order.Features.Order.Dtos;

namespace Order.Features.Order;

public class GetOrdersByTableIdQurey : IRequest<PaginatedList<OrderDto>>
{
    public int TableId { get; set; }
    
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}

public class GetOrdersByTableIdQueryValidator : AbstractValidator<GetOrdersByTableIdQurey>
{
    public GetOrdersByTableIdQueryValidator()
    {
        RuleFor(x => x.TableId)
            .NotEmpty();
        
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class GetOrdersByTableIdRequestHandler : IRequestHandler<GetOrdersByTableIdQurey, PaginatedList<OrderDto>>
{
    private readonly IOrderRepository<Domain.Order> _repository;
    private readonly IMapper _mapper;

    public GetOrdersByTableIdRequestHandler(IOrderRepository<Domain.Order> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<OrderDto>> Handle(GetOrdersByTableIdQurey request, CancellationToken cancellationToken)
    {
        return await _repository.GetOrdersByTableId(request.TableId, request.PageNumber, request.PageSize)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}