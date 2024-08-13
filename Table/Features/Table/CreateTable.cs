using AutoMapper;
using Common.Interfaces.IRepositories.Table;
using Common.Mappings;
using FluentValidation;
using MediatR;

namespace Table.Features.Table;

public class CreateTableCommand : IRequest<int>, IMapFrom<Domain.Table>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public DateTime StartTime { get; set; }
    
    public int PlaceId { get; set; }
    public int GroupId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Table, CreateTableCommand>().ReverseMap();
        }
    }
}

public class CreateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateTableCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(40);
        RuleFor(x => x.Description)
            .MaximumLength(100);
        RuleFor(x => x.GroupId)
            .NotEmpty();
    }
}

internal sealed class CreateTableRequestHandler : IRequestHandler<CreateTableCommand, int>
{
    private readonly ITableRepository<Domain.Table> _repository;
    private readonly IMapper _mapper;

    public CreateTableRequestHandler(ITableRepository<Domain.Table> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var newEntity = new Domain.Table();
        var entity = _mapper.Map(request, newEntity);
        return await _repository.CreateAsync(entity, cancellationToken);
    }
}