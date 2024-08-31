using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;

namespace User.Features.FriendInvite;

public class CreateFriendInviteCommand : IRequest<int>
{
    public string FriendsUsername { get; set; } = null!;
}

public class CreateFriendInviteValidator : AbstractValidator<CreateFriendInviteCommand>
{
    public CreateFriendInviteValidator()
    {
        RuleFor(x => x.FriendsUsername)
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
            FriendsUsername = request.FriendsUsername,
            InvitationAccepted = false
        };
        return await _repository.CreateAsync(entity, cancellationToken);
    }
}