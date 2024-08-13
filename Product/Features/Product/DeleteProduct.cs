using Common.Interfaces;
using Common.Interfaces.IRepositories.Product;
using FluentValidation;
using MediatR;


namespace Product.Features.Product;

public class DeleteProductCommand : IRequest<Task>
{
    public int ProductId { get; set; }
}

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}

internal sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Task>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProductRepository<Domain.Product> _repository;

    public DeleteProductCommandHandler(ICurrentUserService currentUser, IProductRepository<Domain.Product> repository)
    {
        _currentUser = currentUser;
        _repository = repository;
    }

    public async Task<Task> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.ProductId, cancellationToken);
        return Task.CompletedTask;
    }
}