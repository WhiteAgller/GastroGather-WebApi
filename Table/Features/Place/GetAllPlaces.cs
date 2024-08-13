using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces.IRepositories.Table;
using Common.Mappings;
using Common.Models;
using FluentValidation;
using MediatR;
using Table.Features.Place.Dtos;

namespace Table.Features.Place;

public class GetAllPlacesQuery : IRequest<PaginatedList<PlaceDto>>
{
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}

public class GetAllPlacesQueryValidator : AbstractValidator<GetAllPlacesQuery>
{
    public GetAllPlacesQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class GetAllPlacesRequestHandler : IRequestHandler<GetAllPlacesQuery, PaginatedList<PlaceDto>>
{
    private readonly IPlaceRepository<Domain.Place> _repository;
    private readonly IMapper _mapper;

    public GetAllPlacesRequestHandler(IPlaceRepository<Domain.Place> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<PlaceDto>> Handle(GetAllPlacesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.PageNumber, request.PageSize)
            .ProjectTo<PlaceDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}