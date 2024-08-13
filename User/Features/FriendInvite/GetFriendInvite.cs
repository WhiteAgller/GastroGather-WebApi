using AutoMapper;
using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;
using User.Features.FriendInvite.Dtos;
using User.Features.Invite.Dtos;

namespace User.Features.FriendInvite;

public class GetFriendInviteQuery : IRequest<FriendInviteDto>
{
    public int Id { get; set; }
}

public class GetFriendInviteQueryValidator : AbstractValidator<GetFriendInviteQuery>
{
    public GetFriendInviteQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class GetInviteQueryHandler : IRequestHandler<GetFriendInviteQuery, FriendInviteDto>
{
    private readonly IFriendInviteRepository<Domain.FriendInvite> _repository;
    private readonly IMapper _mapper;

    public GetInviteQueryHandler(IFriendInviteRepository<Domain.FriendInvite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FriendInviteDto> Handle(GetFriendInviteQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<Domain.FriendInvite, FriendInviteDto>(entity);
    }
}