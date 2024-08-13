using Common.Exceptions;
using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;

namespace User.Features.Invite;

public class DeleteInviteCommand : IRequest<Task>
{
    public int Id { get; set; }
}

public class DeleteInviteValidator : AbstractValidator<DeleteInviteCommand>
{
    public DeleteInviteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class DeleteInviteCommandHandler : IRequestHandler<DeleteInviteCommand, Task>
{
    private readonly IInviteRepository<Domain.Invite> _repository;

    public DeleteInviteCommandHandler(IInviteRepository<Domain.Invite> repository)
    {
        _repository = repository;
    }

    public async Task<Task> Handle(DeleteInviteCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return Task.CompletedTask;
    }
}