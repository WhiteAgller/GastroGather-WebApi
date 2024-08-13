using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;

namespace User.Features.Invite;

public class CreateInviteCommand : IRequest<int>
{
    public string UserId { get; set; }
    public int GroupId { get; set; }
}

public class CreateInviteValidator : AbstractValidator<CreateInviteCommand>
{
    public CreateInviteValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.GroupId)
            .NotEmpty();
    }
}

internal sealed class CreateInviteCommandHandler : IRequestHandler<CreateInviteCommand, int>
{
    private readonly IInviteRepository<Domain.Invite> _repository;

    public CreateInviteCommandHandler(IInviteRepository<Domain.Invite> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Invite()
        {
            GroupId = request.GroupId,
            UserId = request.UserId,
            InvitationAccepted = false
        };
        return await _repository.CreateAsync(entity, cancellationToken);
    }
}