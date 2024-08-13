using Common.Interfaces.IRepositories.Order;
using FluentValidation;
using MediatR;

namespace Order.Features.OrderItem;

public class CreateOrderItemCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
}

public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}

internal sealed class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, int>
{
    private readonly IOrderItemRepository<Domain.OrderItem> _repository;

    public CreateOrderItemCommandHandler(IOrderItemRepository<Domain.OrderItem> repository)
    {
       _repository = repository;
    }

    public async Task<int> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = new Domain.OrderItem()
        {
            ProductId = request.ProductId,
            OrderId = request.OrderId,
            Amount = 0,
        };

        return await _repository.CreateAsync(orderItem, cancellationToken);
    }
}