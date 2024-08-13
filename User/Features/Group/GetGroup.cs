using AutoMapper;
using Common.Interfaces.IRepositories.User;
using FluentValidation;
using MediatR;
using User.Features.Group.Dtos;

namespace User.Features.Group;

public class GetGroupQuery : IRequest<GroupDto>
{
    public int Id { get; set; }
}

public class GetGroupQueryValidator : AbstractValidator<GetGroupQuery>
{
    public GetGroupQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto>
{
    private readonly IGroupRepository<Domain.Group> _repository;
    private readonly IMapper _mapper;

    public GetGroupQueryHandler(IGroupRepository<Domain.Group> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GroupDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<Domain.Group, GroupDto>(group);
    }
}