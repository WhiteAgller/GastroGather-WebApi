using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;

namespace User.Features.FriendInvite;

public class CreateFriendInviteCommand : IRequest<int>
{
    public string UserId { get; set; }
    public string FriendUnique { get; set; }
}

public class CreateFriendInviteValidator : AbstractValidator<CreateFriendInviteCommand>
{
    public CreateFriendInviteValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.FriendUnique)
            .NotEmpty();
    }
}

internal sealed class CreateFriendInviteCommandHandler : IRequestHandler<CreateFriendInviteCommand, int>
{
    private readonly IFriendInviteRepository<Domain.FriendInvite> _repository;

    public CreateFriendInviteCommandHandler(IFriendInviteRepository<Domain.FriendInvite> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateFriendInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.FriendInvite()
        {
            UserId = request.UserId,
            FriendUnique = request.FriendUnique,
            InvitationAccepted = false
        };
        return await _repository.CreateAsync(entity, cancellationToken);
    }
}