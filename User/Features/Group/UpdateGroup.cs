using AutoMapper;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using FluentValidation;
using MediatR;
using User.Features.Group.Dtos;
using User.Features.GroupInvite.Dtos;

namespace User.Features.Group;

public class UpdateGroupCommand : UpdateGroupRequestBody, IRequest<GroupDto>, IMapFrom<Domain.Group>
{
    public int Id { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Group, UpdateGroupCommand>().ReverseMap();
        }
    }
}

public class UpdateGroupRequestBody
{
    public string Name { get; set; } = null!;
    public int MaxNumberOfPeople { get; set; }
    public String AdminUserId { get; set; } = null!;
    
    public List<GroupInviteDto> Invites { get; set; } = new List<GroupInviteDto>();
}

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.AdminUserId)
            .NotEmpty();
        RuleFor(x => x.Name)
            .MaximumLength(40);
        RuleFor(x => x.MaxNumberOfPeople)
            .GreaterThan(0);
    }
}

internal sealed class UpdateGroupRequestHandler : IRequestHandler<UpdateGroupCommand, GroupDto>
{
    private readonly IGroupRepository<Domain.Group> _repository;
    private readonly IMapper _mapper;

    public UpdateGroupRequestHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<GroupDto> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var updatedGroup = _mapper.Map(request, group);
        var result = await _repository.UpdateAsync(updatedGroup, cancellationToken);
        return _mapper.Map<Domain.Group, GroupDto>(result);
    }
}

