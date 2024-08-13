using Common;
using Common.Interfaces;
using Common.Interfaces.IRepositories.Product;
using FluentValidation;
using MediatR;

namespace Product.Features.Product;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(30)
            .NotEmpty();
        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}

internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProductRepository<Domain.Product> _repository;

    public CreateProductCommandHandler(ICurrentUserService currentUser, IProductRepository<Domain.Product> repository)
    {
        _currentUser = currentUser;
        _repository = repository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Product()
        {
            Name = request.Name,
            CategoryId = 1
        };
        
        entity.DomainEvents.Add(new ProductCreatedEvent(entity));
        
        await _repository.CreateAsync(entity, cancellationToken);
        
        return entity.Id;
    }
}

internal sealed class ProductCreatedEvent : DomainEvent
{
    public ProductCreatedEvent(Domain.Product entity)
    {
        
    }
}