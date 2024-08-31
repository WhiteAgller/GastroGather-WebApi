using AutoMapper;
using Common.Exceptions;
using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;
using User.Features.GroupInvite.Dtos;

namespace User.Features.Invite;

public class GetInviteQuery : IRequest<GroupInviteDto>
{
    public int Id { get; set; }
}

public class GetInviteQueryValidator : AbstractValidator<GetInviteQuery>
{
    public GetInviteQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class GetInviteQueryHandler : IRequestHandler<GetInviteQuery, GroupInviteDto>
{
    private readonly IGroupInviteRepository<Domain.GroupInvite> _repository;
    private readonly IMapper _mapper;

    public GetInviteQueryHandler(IGroupInviteRepository<Domain.GroupInvite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GroupInviteDto> Handle(GetInviteQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<Domain.GroupInvite, GroupInviteDto>(entity);
    }
}