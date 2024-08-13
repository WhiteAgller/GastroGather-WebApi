using AutoMapper;
using Common.Exceptions;
using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;
using User.Features.Invite.Dtos;

namespace User.Features.Invite;

public class GetInviteQuery : IRequest<InviteDto>
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

internal sealed class GetInviteQueryHandler : IRequestHandler<GetInviteQuery, InviteDto>
{
    private readonly IInviteRepository<Domain.Invite> _repository;
    private readonly IMapper _mapper;

    public GetInviteQueryHandler(IInviteRepository<Domain.Invite> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<InviteDto> Handle(GetInviteQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<Domain.Invite, InviteDto>(entity);
    }
}