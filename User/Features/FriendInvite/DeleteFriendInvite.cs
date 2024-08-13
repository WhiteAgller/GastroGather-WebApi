using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;
using User.Features.Invite;

namespace User.Features.FriendInvite;

public class DeleteFriendInviteCommand : IRequest<Task>
{
    public int Id { get; set; }
}

public class DeleteFriendInviteValidator : AbstractValidator<DeleteInviteCommand>
{
    public DeleteFriendInviteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class DeleteFriendInviteCommandHandler : IRequestHandler<DeleteInviteCommand, Task>
{
    private readonly IFriendInviteRepository<Domain.FriendInvite> _repository;

    public DeleteFriendInviteCommandHandler(IFriendInviteRepository<Domain.FriendInvite> repository)
    {
        _repository = repository;
    }

    public async Task<Task> Handle(DeleteInviteCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return Task.CompletedTask;
    }
}