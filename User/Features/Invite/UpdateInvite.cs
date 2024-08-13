using AutoMapper;
using Common.Exceptions;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using FluentValidation;
using MediatR;
using User.Features.Invite.Dtos;

namespace User.Features.Invite;

public class UpdateInviteCommand : UpdateInviteRequestBody, IRequest<InviteDto>, IMapFrom<Domain.Invite>
{
    public int Id { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Invite, UpdateInviteCommand>().ReverseMap();
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

internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateInviteCommand, InviteDto>
{
    private readonly IInviteRepository<Domain.Invite> _repository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IInviteRepository<Domain.Invite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<InviteDto> Handle(UpdateInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var newEntity = _mapper.Map(request, entity);
        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);
        return _mapper.Map<User.Domain.Invite, InviteDto>(updatedEntity);
    }
}