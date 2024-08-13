using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;

namespace User.Features.Group;

public class DeleteGroupCommand : IRequest<Task>
{
    public int Id { get; set; }
}

public class DeleteGroupQueryValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class DeleteGroupQueryHandler : IRequestHandler<DeleteGroupCommand, Task>
{
    private readonly IGroupRepository<Domain.Group> _repository;

    public DeleteGroupQueryHandler(IGroupRepository<Domain.Group> repository)
    {
        _repository = repository;
    }

    public async Task<Task> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return Task.CompletedTask;
    }
}