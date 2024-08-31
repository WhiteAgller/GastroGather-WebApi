using AutoMapper;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using FluentValidation;
using MediatR;
using User.Features.GroupInvite.Dtos;

namespace User.Features.GroupInvite;

public class UpdateInviteCommand : UpdateInviteRequestBody, IRequest<GroupInviteDto>, IMapFrom<Domain.GroupInvite>
{
    public int Id { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.GroupInvite, UpdateInviteCommand>().ReverseMap();
            CreateMap<UpdateInviteCommand, UpdateInviteRequestBody>().ReverseMap()
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }
}

public class UpdateInviteRequestBody
{
    public string UserId { get; set; }
    
    public int GroupId { get; set; }
    
    public bool InvitationAccepted { get; set; }
}

public class UpdateInviteCommandValidator : AbstractValidator<UpdateInviteCommand>
{
    public UpdateInviteCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.GroupId)
            .NotEmpty();
        RuleFor(x => x.InvitationAccepted)
            .NotEmpty();
    }
}

internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateInviteCommand, GroupInviteDto>
{
    private readonly IGroupInviteRepository<Domain.GroupInvite> _repository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IGroupInviteRepository<Domain.GroupInvite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<GroupInviteDto> Handle(UpdateInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var newEntity = _mapper.Map(request, entity);
        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);
        return _mapper.Map<Domain.GroupInvite, GroupInviteDto>(updatedEntity);
    }
}