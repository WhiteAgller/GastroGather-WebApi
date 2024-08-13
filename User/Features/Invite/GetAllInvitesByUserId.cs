using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces.IRepositories.User;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using User.Features.Invite.Dtos;

namespace User.Features.Invite;

public class GetAllInvitesByUserIdQuery : IRequest<PaginatedList<InviteDto>>
{
    public string Username { get; set; } = null!;
    
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

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("User Id should not be null");
    }
}

internal sealed class
    GetAllInvitesByUserIdQueryHandler : IRequestHandler<GetAllInvitesByUserIdQuery, PaginatedList<InviteDto>>
{
    private readonly IInviteRepository<Domain.Invite> _repository;
    private readonly IMapper _mapper;

    public GetAllInvitesByUserIdQueryHandler(IInviteRepository<Domain.Invite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InviteDto>> Handle(GetAllInvitesByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllByUserName(request.Username, request.PageNumber, request.PageSize)
            .ProjectTo<InviteDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}