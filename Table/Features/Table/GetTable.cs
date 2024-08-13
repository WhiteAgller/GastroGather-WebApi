using AutoMapper;
using Common.Interfaces.IRepositories.Table;
using FluentValidation;
using MediatR;
using Table.Features.Table.Dtos;


namespace Table.Features.Table;

public class GetTableQuery : IRequest<TableDto>
{
    public int TableId { get; set; }
}

public class GetTableQueryValidator : AbstractValidator<GetTableQuery>
{
    public GetTableQueryValidator()
    {
        RuleFor(x => x.TableId)
            .NotEmpty();
    }
}

internal sealed class GetTableRequestHandler : IRequestHandler<GetTableQuery, TableDto>
{
    private readonly ITableRepository<Domain.Table> _repository;
    private readonly IMapper _mapper;

    public GetTableRequestHandler(ITableRepository<Domain.Table> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<TableDto> Handle(GetTableQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.TableId, cancellationToken);
        return _mapper.Map<Domain.Table, TableDto>(entity);
    }
}