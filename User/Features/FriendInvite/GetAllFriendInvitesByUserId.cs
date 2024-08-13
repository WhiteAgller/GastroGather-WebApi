using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using User.Features.FriendInvite.Dtos;
using User.Features.Invite.Dtos;

namespace User.Features.FriendInvite;

public class GetAllFriendInvitesByUserIdQuery : IRequest<PaginatedList<FriendInviteDto>>
{
    public string Username { get; set; } = null!;
    
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}

public class GetAllInvitesByOrderIdQueryValidator : AbstractValidator<GetAllFriendInvitesByUserIdQuery>
{
    public GetAllInvitesByOrderIdQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username should not be null");
    }
}

internal sealed class
    GetAllFriendInvitesByUserIdQueryHandler : IRequestHandler<GetAllFriendInvitesByUserIdQuery, PaginatedList<FriendInviteDto>>
{
    private readonly IFriendInviteRepository<Domain.FriendInvite> _repository;
    private readonly IMapper _mapper;

    public GetAllFriendInvitesByUserIdQueryHandler(IFriendInviteRepository<Domain.FriendInvite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<FriendInviteDto>> Handle(GetAllFriendInvitesByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllByUserName(request.Username, request.PageNumber, request.PageSize)
            .ProjectTo<FriendInviteDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}