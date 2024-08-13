using AutoMapper;
using Common.Interfaces.IRepositories.Order;
using Common.Mappings;
using FluentValidation;
using MediatR;
using Order.Features.OrderItem.Dtos;

namespace Order.Features.Order;

public class CreateOrderCommand : IRequest<int>, IMapFrom<Domain.Order>
{
    public int TableId { get; set; }
    public string UserId { get; set; }
    public decimal TotalCost { get; set; } = 0;
    
    public List<OrderItemDto> OrderItems { get; set; } = null!;
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Order, CreateOrderCommand>().ReverseMap();
        }
    }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.TableId)
            .NotEmpty();
        RuleFor(x => x.TotalCost)
            .GreaterThanOrEqualTo(0);
    }
}

internal sealed class CreateCommandRequestHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderRepository<Domain.Order> _repository;
    private readonly IMapper _mapper;

    public CreateCommandRequestHandler(IOrderRepository<Domain.Order> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newEntity = new Domain.Order();
        var entity = _mapper.Map(request, newEntity);
        return await _repository.CreateAsync(entity, cancellationToken);
    }
} 