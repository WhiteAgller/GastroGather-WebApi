using AutoMapper;
using Common.Interfaces.IRepositories.Order;
using FluentValidation;
using MediatR;
using Order.Features.Order.Dtos;


namespace Order.Features.Order;

public class GetOrderQuery : IRequest<OrderDto>
{
    public int Id { get; set; }
}

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class GetOrderRequestHandler : IRequestHandler<GetOrderQuery, OrderDto>
{
    private readonly IOrderRepository<Domain.Order> _repository;
    private readonly IMapper _mapper;

    public GetOrderRequestHandler(IOrderRepository<Domain.Order> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<Domain.Order, OrderDto>(entity);
    }
}