using AutoMapper;
using Common.Exceptions;
using Common.Interfaces.IRepositories.Order;
using Common.Mappings;
using FluentValidation;
using MediatR;
using Order.Features.Order.Dtos;
using Order.Features.OrderItem.Dtos;

namespace Order.Features.Order;

public class UpdateOrderCommand : UpdateOrderRequestBody, IRequest<OrderDto>, IMapFrom<Domain.Order>
{
    public int Id { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Order, UpdateOrderCommand>();
        }
    }
}

public class UpdateOrderRequestBody
{
    public int TableId { get; set; }
    public int UserId { get; set; }
    
    public List<OrderItemDto> OrderItemsList { get; set; } = null!;
    public decimal TotalCost { get; set; }
}

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.TableId)
            .NotEmpty();
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.TotalCost)
            .GreaterThanOrEqualTo(0);
    }
}

internal sealed class UpdateOrderRequestHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IOrderRepository<Domain.Order> _repository;
    private readonly IMapper _mapper;

    public UpdateOrderRequestHandler(IOrderRepository<Domain.Order> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var newOrder =_mapper.Map(request, entity);
        var result = await _repository.UpdateAsync(newOrder, cancellationToken);
        return _mapper.Map<Domain.Order, OrderDto>(result);
    }
}