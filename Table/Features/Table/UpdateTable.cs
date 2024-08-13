using AutoMapper;
using Common.Interfaces.IRepositories.Table;
using Common.Mappings;
using FluentValidation;
using MediatR;
using Table.Features.Table.Dtos;

namespace Table.Features.Table;

public class UpdateTableCommand : UpdateTableRequestBody, IRequest<TableDto>, IMapFrom<Domain.Table>
{
    public int Id { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Table, UpdateTableCommand>().ReverseMap();
            CreateMap<Domain.Table, UpdateTableRequestBody>().ReverseMap()
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }
}

public class UpdateTableRequestBody
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public DateTime StartTime { get; set; }
    public int PlaceId { get; set; }
    public int GroupId { get; set; }
}

public class UpdateTableCommandValidator : AbstractValidator<UpdateTableCommand>
{
    public UpdateTableCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.Name)
            .MaximumLength(40);
        RuleFor(x => x.Description)
            .MaximumLength(100);
        RuleFor(x => x.PlaceId)
            .NotEmpty();
        RuleFor(x => x.GroupId)
            .NotEmpty();
    }
}

public class UpdateTableRequestHandler : IRequestHandler<UpdateTableCommand, TableDto>
{
    private readonly ITableRepository<Domain.Table> _repository;
    private readonly IMapper _mapper;

    public UpdateTableRequestHandler(ITableRepository<Domain.Table> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TableDto> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var newOrder =_mapper.Map(request, entity);
        var result = await _repository.UpdateAsync(newOrder, cancellationToken);
        return _mapper.Map<Domain.Table, TableDto>(result);
    }
}
