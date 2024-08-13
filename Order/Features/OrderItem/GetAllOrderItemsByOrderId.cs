using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces.IRepositories.Order;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using Order.Features.OrderItem.Dtos;

namespace Order.Features.OrderItem;

public class GetAllOrderItemsByOrderIdQuery : IRequest<PaginatedList<OrderItemDto>>
{
    public int OrderItemId { get; set; }
    
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}

public class GetAllOrderItemsByOrderIdQueryValidator : AbstractValidator<GetAllOrderItemsByOrderIdQuery>
{
    public GetAllOrderItemsByOrderIdQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(x => x.OrderItemId)
            .NotEmpty().WithMessage("Order item should not be null");
    }
}

internal sealed class
    GetAllOrderItemsByOrderIdQueryHandler : IRequestHandler<GetAllOrderItemsByOrderIdQuery, PaginatedList<OrderItemDto>>
{
    private readonly IOrderItemRepository<Domain.OrderItem> _repository;
    private readonly IMapper _mapper;

    public GetAllOrderItemsByOrderIdQueryHandler(IOrderItemRepository<Domain.OrderItem> repository, IMapper mapper)
    {
       _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderItemDto>> Handle(GetAllOrderItemsByOrderIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllOrderItemsByOrderId(request.OrderItemId, request.PageNumber, request.PageSize)
            .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}