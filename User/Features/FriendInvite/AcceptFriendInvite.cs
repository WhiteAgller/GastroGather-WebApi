using AutoMapper;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using FluentValidation;
using MediatR;
using User.Features.FriendInvite.Dtos;

namespace User.Features.FriendInvite;

public class AcceptFriendInviteCommand : IRequest<FriendInviteDto>, IMapFrom<Domain.FriendInvite>
{
    public int Id { get; set; }
}

public class AcceptFriendInviteCommandValidator : AbstractValidator<AcceptFriendInviteCommand>
{
    public AcceptFriendInviteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

internal sealed class AcceptFriendInviteCommandHandler : IRequestHandler<AcceptFriendInviteCommand, FriendInviteDto>
{
    private readonly IFriendInviteRepository<Domain.FriendInvite> _repository;
    private readonly IMapper _mapper;

    public AcceptFriendInviteCommandHandler(IFriendInviteRepository<Domain.FriendInvite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FriendInviteDto> Handle(AcceptFriendInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        entity.InvitationAccepted = true;
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        return _mapper.Map<Domain.FriendInvite, FriendInviteDto>(updatedEntity);
    }
}