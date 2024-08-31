using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using User.Features.GroupInvite.Dtos;

namespace User.Features.GroupInvite;

public class GetAllInvitesByUserIdQuery : IRequest<PaginatedList<GroupInviteDto>>
{
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}

public class GetAllInvitesByOrderIdQueryValidator : AbstractValidator<GetAllInvitesByUserIdQuery>
{
    public GetAllInvitesByOrderIdQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class
    GetAllInvitesByUserIdQueryHandler : IRequestHandler<GetAllInvitesByUserIdQuery, PaginatedList<GroupInviteDto>>
{
    private readonly IGroupInviteRepository<Domain.GroupInvite> _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetAllInvitesByUserIdQueryHandler(IGroupInviteRepository<Domain.GroupInvite> repository, IMapper mapper, ICurrentUserService currentUser)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<PaginatedList<GroupInviteDto>> Handle(GetAllInvitesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = _currentUser.UserId;
        return await _repository.GetAllByUserName(user, request.PageNumber, request.PageSize)
            .ProjectTo<GroupInviteDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}