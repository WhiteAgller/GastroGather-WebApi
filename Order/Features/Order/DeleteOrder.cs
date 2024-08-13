using Common.Interfaces.IRepositories.Order;
using FluentValidation;
using MediatR;

namespace Order.Features.Order;

public class DeleteOrderCommand : IRequest<Task>
{
    public int Id { get; set; }
}

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderCommand, Task>
{
    private readonly IOrderRepository<Domain.Order> _repository;

    public DeleteOrderRequestHandler(IOrderRepository<Domain.Order> repository)
    {
        _repository = repository;
    }

    public async Task<Task> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return Task.CompletedTask;
    }
}