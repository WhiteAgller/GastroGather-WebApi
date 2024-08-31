using AutoMapper;
using Common;
using Common.Interfaces;
using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace User.Features.Group;

public class CreateGroupCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public List<string> UserNames { get; set; } = new List<string>();
}

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(g => g.Name)
            .MaximumLength(25)
            .NotEmpty();
    }
}

internal sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, int>
{
    private readonly IGroupRepository<Domain.Group> _repository;
    private readonly UserManager<Domain.User> _userManager;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public CreateGroupCommandHandler(ICurrentUserService currentUser, IGroupRepository<Domain.Group> repository, IMapper mapper, UserManager<Domain.User> userManager)
    {
        _currentUser = currentUser;
        _repository = repository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {

        List<Domain.GroupInvite> groupInvites = new List<Domain.GroupInvite>();
        var currentUser = await _userManager.FindByIdAsync(_currentUser.UserId);
        var entity = new Domain.Group()
        {
            Name = request.Name,
            AdminUserName = _currentUser.UserId ?? "default",
        };
        foreach (var userName in request.UserNames)
        {
            var friend = await _userManager.FindByNameAsync(userName);

            if (friend is not null)
            {

                var groupInvite = new Domain.GroupInvite()
                {
                    User = currentUser,
                    Friend = friend,
                    Group = entity,
                    FriendsUsername = friend.UserName,
                    InvitationAccepted = false
                };
                groupInvites.Add(groupInvite);
            }
        }

        entity.GroupInvites = groupInvites;
        return await _repository.CreateAsync(entity, cancellationToken);
    }
}

internal sealed class GroupCreatedEvent : DomainEvent
{
    public GroupCreatedEvent(Domain.Group entity)
    {
        
    }
}