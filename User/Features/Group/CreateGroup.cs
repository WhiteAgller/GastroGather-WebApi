using AutoMapper;
using Common;
using Common.Interfaces;
using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;
using User.Features.Invite.Dtos;

namespace User.Features.Group;

public class CreateGroupCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public int MaxNumberOfPeople { get; set; } = 4;
    public String AdminUserId { get; set; } = null!;
    public List<InviteDto> Invites { get; set; } = new List<InviteDto>();
}

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(g => g.Name)
            .MaximumLength(25)
            .NotEmpty();
        RuleFor(g => g.AdminUserId)
            .NotEmpty();
        RuleFor(g => g.MaxNumberOfPeople)
            .GreaterThan(0);
    }
}

internal sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, int>
{
    private readonly IGroupRepository<Domain.Group> _repository;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public CreateGroupCommandHandler(ICurrentUserService currentUser, IGroupRepository<Domain.Group> repository, IMapper mapper)
    {
        _currentUser = currentUser;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Group()
        {
            Name = request.Name,
            MaxNumberOfPeople = 1,
            AdminUserId = _currentUser.UserId ?? "default",
            Invites = _mapper.Map<List<InviteDto>, List<Domain.Invite>>(request.Invites)
            
        };
        return await _repository.CreateAsync(entity, cancellationToken);
    }
}

internal sealed class GroupCreatedEvent : DomainEvent
{
    public GroupCreatedEvent(Domain.Group entity)
    {
        
    }
}