using AutoMapper;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using FluentValidation;
using MediatR;
using User.Features.GroupInvite.Dtos;


namespace User.Features.GroupInvite;

public class AcceptGroupInviteCommand : IRequest<GroupInviteDto>, IMapFrom<Domain.GroupInvite>
{
    public int Id { get; set; }
}

public class AcceptGroupInviteCommandValidator : AbstractValidator<AcceptGroupInviteCommand>
{
    public AcceptGroupInviteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

internal sealed class AcceptGroupInviteCommandHandler : IRequestHandler<AcceptGroupInviteCommand, GroupInviteDto>
{
    private readonly IGroupInviteRepository<Domain.GroupInvite> _repository;
    private readonly IMapper _mapper;

    public AcceptGroupInviteCommandHandler(IGroupInviteRepository<Domain.GroupInvite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GroupInviteDto> Handle(AcceptGroupInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        entity.InvitationAccepted = true;
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        return _mapper.Map<Domain.GroupInvite, GroupInviteDto>(updatedEntity);
    }
}