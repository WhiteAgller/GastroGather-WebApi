using AutoMapper;
using Common.Interfaces.IRepositories.Order;
using Common.Models;
using FluentValidation;
using MediatR;
using Order.Features.Order.Dtos;

namespace Order.Features.Order;

public class GetOrdersByUserIdQurey : IRequest<PaginatedList<OrderDto>>
{
    public int UserId { get; set; }
    
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}

public class GetOrdersByUserIdQueryValidator : AbstractValidator<GetOrdersByUserIdQurey>
{
    public GetOrdersByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class GetOrdersByUserIdRequestHandler : IRequestHandler<GetOrdersByUserIdQurey, PaginatedList<OrderDto>>
{
    private readonly IOrderRepository<Domain.Order> _repository;
    private readonly IMapper _mapper;

    public GetOrdersByUserIdRequestHandler(IOrderRepository<Domain.Order> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<OrderDto>> Handle(GetOrdersByUserIdQurey request, CancellationToken cancellationToken)
    {
        return null;
    }
}