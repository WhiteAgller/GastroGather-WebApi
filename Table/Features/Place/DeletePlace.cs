using Common.Interfaces.IRepositories.Table;
using FluentValidation;
using MediatR;

namespace Table.Features.Place;

public class DeletePlaceCommand : IRequest<Task>
{
    public int Id { get; set; }
}

public class DeletePlaceCommandValidator : AbstractValidator<DeletePlaceCommand>
{
    public DeletePlaceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class DeletePlaceRequestHandler : IRequestHandler<DeletePlaceCommand, Task>
{
    private readonly IPlaceRepository<Domain.Place> _repository;

    public DeletePlaceRequestHandler(IPlaceRepository<Domain.Place> repository)
    {
        _repository = repository;
    }
    
    public async Task<Task> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return Task.CompletedTask;
    }
}