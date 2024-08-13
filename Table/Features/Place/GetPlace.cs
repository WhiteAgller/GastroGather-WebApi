using AutoMapper;
using Common.Exceptions;
using Common.Interfaces.IRepositories.Table;
using FluentValidation;
using MediatR;
using Table.Features.Place.Dtos;

namespace Table.Features.Place;

public class GetPlaceQuery : IRequest<PlaceDto>
{
    public int Id { get; set; }
}

public class GetPlaceQueryValidator : AbstractValidator<GetPlaceQuery>
{
    public GetPlaceQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal sealed class GetPlaceRequestHandler : IRequestHandler<GetPlaceQuery, PlaceDto>
{
    private readonly IPlaceRepository<Domain.Place> _repository;
    private readonly IMapper _mapper;

    public GetPlaceRequestHandler(IPlaceRepository<Domain.Place> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<PlaceDto> Handle(GetPlaceQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<Domain.Place, PlaceDto>(entity);
    }
}