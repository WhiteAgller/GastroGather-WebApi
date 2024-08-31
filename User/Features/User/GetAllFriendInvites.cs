using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using User.Features.User.Dtos;

namespace User.Features.User;

public class GetAllFriendInvitesQuery : IRequest<PaginatedList<UserDto>>
{
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}

public class GetAllFriendsInvitesQueryValidator : AbstractValidator<GetAllFriendInvitesQuery>
{
    public GetAllFriendsInvitesQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class
    GetAllFriendInvitesQueryHandler : IRequestHandler<GetAllFriendInvitesQuery, PaginatedList<UserDto>>
{
    private readonly IUserRepository<Domain.User> _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetAllFriendInvitesQueryHandler(IUserRepository<Domain.User> repository, IMapper mapper, ICurrentUserService currentUser)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<PaginatedList<UserDto>> Handle(GetAllFriendInvitesQuery request, CancellationToken cancellationToken)
    {
        var user = _currentUser.UserId;
        return await _repository.GetAllFriendInvites(user, request.PageNumber, request.PageSize)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}