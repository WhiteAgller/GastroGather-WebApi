using AutoMapper;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using FluentValidation;
using MediatR;
using User.Features.FriendInvite.Dtos;

namespace User.Features.FriendInvite;

public class UpdateFriendInviteCommand : UpdateFriendInviteRequestBody, IRequest<FriendInviteDto>, IMapFrom<Domain.FriendInvite>
{
    public int Id { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.FriendInvite, UpdateFriendInviteCommand>().ReverseMap();
            CreateMap<UpdateFriendInviteCommand, UpdateFriendInviteRequestBody>().ReverseMap()
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }
}

public class UpdateFriendInviteRequestBody
{
    public string UserId { get; set; }
    
    public string FriendUnique { get; set; }
    
    public bool InvitationAccepted { get; set; }
}

public class UpdateInviteCommandValidator : AbstractValidator<UpdateFriendInviteCommand>
{
    public UpdateInviteCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.FriendUnique)
            .NotEmpty();
        RuleFor(x => x.InvitationAccepted)
            .NotEmpty();
    }
}

internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateFriendInviteCommand, FriendInviteDto>
{
    private readonly IFriendInviteRepository<Domain.FriendInvite> _repository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IFriendInviteRepository<Domain.FriendInvite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<FriendInviteDto> Handle(UpdateFriendInviteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var newEntity = _mapper.Map(request, entity);
        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);
        return _mapper.Map<Domain.FriendInvite, FriendInviteDto>(updatedEntity);
    }
}