using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;

namespace User.Features.GroupInvite;

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
    private readonly IGroupInviteRepository<Domain.GroupInvite> _repository;

    public CreateInviteCommandHandler(IGroupInviteRepository<Domain.GroupInvite> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.GroupInvite()
        {
            GroupId = request.GroupId,
            InvitationAccepted = false
        };
        return await _repository.CreateAsync(entity, cancellationToken);
    }
}