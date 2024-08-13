using Common.Interfaces.IRepositories.Order;
using FluentValidation;
using MediatR;

namespace Order.Features.OrderItem;

public class DeleteOrderItemCommand : IRequest<Task>
{
    public int Id { get; set; }
}

public class DeleteOrderItemValidator : AbstractValidator<DeleteOrderItemCommand>
{
    public DeleteOrderItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Task>
{
    private readonly IOrderItemRepository<Domain.OrderItem> _repository;

    public DeleteOrderItemCommandHandler(IOrderItemRepository<Domain.OrderItem> repository)
    {
       _repository = repository;
    }

    public async Task<Task> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return Task.CompletedTask;
    }
}