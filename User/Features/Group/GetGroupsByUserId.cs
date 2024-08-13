using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using User.Features.Group.Dtos;

namespace User.Features.Group;

public class GetGroupsByUserIdQuery : IRequest<PaginatedList<GroupDto>>
{
    public string Username { get; set; } = null!;
    
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class GetGroupsByUserIdQueryValidator : AbstractValidator<GetGroupsByUserIdQuery>
{
    public GetGroupsByUserIdQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        
        RuleFor(x => x.Username).NotEmpty();
    }
}

internal sealed class GetGroupsByUserIdRequestHandler : IRequestHandler<GetGroupsByUserIdQuery, PaginatedList<GroupDto>>
{
    private readonly IGroupRepository<Domain.Group> _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetGroupsByUserIdRequestHandler(IMapper mapper, ICurrentUserService currentUser, IGroupRepository<Domain.Group> repository)
    {
        _mapper = mapper;
        _currentUser = currentUser;
        _repository = repository;
    }

    public async Task<PaginatedList<GroupDto>> Handle(GetGroupsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllByUserName(request.Username, request.PageNumber, request.PageSize)
            .ProjectTo<GroupDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}