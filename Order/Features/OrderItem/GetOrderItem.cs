using AutoMapper;
using Common.Interfaces.IRepositories.Order;
using FluentValidation;
using MediatR;
using Order.Features.OrderItem.Dtos;

namespace Order.Features.OrderItem;

public class GetOrderItemQuery : IRequest<OrderItemDto>
{
    public int Id { get; set; }
}

public class GetOrderItemQueryValidator : AbstractValidator<GetOrderItemQuery>
{
    public GetOrderItemQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, OrderItemDto>
{
    private readonly IOrderItemRepository<Domain.OrderItem> _repository;
    private readonly IMapper _mapper;

    public GetOrderItemQueryHandler(IOrderItemRepository<Domain.OrderItem> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderItemDto> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
    {
        var orderItem = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<Domain.OrderItem, OrderItemDto>(orderItem);
    }
}