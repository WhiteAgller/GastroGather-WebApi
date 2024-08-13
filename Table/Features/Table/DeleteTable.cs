using Common.Interfaces.IRepositories.Table;
using FluentValidation;
using MediatR;

namespace Table.Features.Table;

public class DeleteTableCommand : IRequest<Task>
{
    public int Id { get; set; }
}

public class DeleteTableCommandValidator : AbstractValidator<DeleteTableCommand>
{
    public DeleteTableCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class DeleteTableRequestHandler : IRequestHandler<DeleteTableCommand, Task>
{
    private readonly ITableRepository<Domain.Table> _repository;

    public DeleteTableRequestHandler(ITableRepository<Domain.Table> repository)
    {
        _repository = repository;
    }
    
    public async Task<Task> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return Task.CompletedTask;
    }
}