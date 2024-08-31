using AutoMapper;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using FluentValidation;
using MediatR;
using User.Features.GroupInvite.Dtos;

namespace User.Features.GroupInvite;

public class DeclineGroupInviteCommand : IRequest<GroupInviteDto>, IMapFrom<Domain.GroupInvite>
{
    public int Id { get; set; }
}

public class DeclineGroupInviteCommandValidator : AbstractValidator<DeclineGroupInviteCommand>
{
    public DeclineGroupInviteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

internal sealed class DeclineGroupInviteCommandHandler : IRequestHandler<DeclineGroupInviteCommand, GroupInviteDto>
{
    private readonly IGroupInviteRepository<Domain.GroupInvite> _repository;
    private readonly IMapper _mapper;

    public DeclineGroupInviteCommandHandler(IGroupInviteRepository<Domain.GroupInvite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GroupInviteDto> Handle(DeclineGroupInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        entity.InvitationAccepted = false;
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        return _mapper.Map<Domain.GroupInvite, GroupInviteDto>(updatedEntity);
    }
}