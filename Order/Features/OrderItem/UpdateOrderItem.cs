using AutoMapper;
using Common.Interfaces.IRepositories.Order;
using Common.Mappings;
using FluentValidation;
using MediatR;
using Order.Features.OrderItem.Dtos;

namespace Order.Features.OrderItem;

public class UpdateOrderItemCommand : UpdateOrderItemRequestBody, IRequest<OrderItemDto>, IMapFrom<Domain.OrderItem>
{
    public int Id { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.OrderItem, UpdateOrderItemCommand>().ReverseMap();
        }
    }
}

public class UpdateOrderItemRequestBody
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public decimal TotalPrice { get; set; }
}

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
        RuleFor(x => x.OrderId)
            .NotEmpty();
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.TotalPrice)
            .GreaterThanOrEqualTo(0);
    }
}

internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderItemCommand, OrderItemDto>
{
    private readonly IOrderItemRepository<Domain.OrderItem> _repository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderItemRepository<Domain.OrderItem> repository, IMapper mapper)
    {
       _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<OrderItemDto> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var entity = _mapper.Map(request, orderItem);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        return _mapper.Map<Domain.OrderItem, OrderItemDto>(result);
    }
}