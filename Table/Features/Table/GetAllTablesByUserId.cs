using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces.IRepositories.Table;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using Table.Features.Table.Dtos;

namespace Table.Features.Table;

public class GetAllTablesByUserIdQuery : IRequest<PaginatedList<TableDto>>
{
    public string Username { get; set; }
    
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}

public class GetAllTablesByUserIdQueryValidator : AbstractValidator<GetAllTablesByUserIdQuery>
{
    public GetAllTablesByUserIdQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty();
        
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class GetAllTablesByUserIdQueryRequestHandler : IRequestHandler<GetAllTablesByUserIdQuery, PaginatedList<TableDto>>
{
    private readonly ITableRepository<Domain.Table> _repository;
    private readonly IMapper _mapper;

    public GetAllTablesByUserIdQueryRequestHandler(ITableRepository<Domain.Table> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TableDto>> Handle(GetAllTablesByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllTablesByUserName(request.Username, request.PageNumber, request.PageSize)
            .ProjectTo<TableDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}