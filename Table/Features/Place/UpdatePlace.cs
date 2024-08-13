using AutoMapper;
using Common.Exceptions;
using Common.Interfaces.IRepositories.Table;
using Common.Mappings;
using FluentValidation;
using MediatR;
using Table.Features.Place.Dtos;

namespace Table.Features.Place;

public class UpdatePlaceCommand : UpdatePlaceRequestBody, IRequest<PlaceDto>, IMapFrom<Domain.Place>
{
    public int Id { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Place, UpdatePlaceCommand>().ReverseMap();
            CreateMap<Domain.Place, UpdatePlaceRequestBody>().ReverseMap()
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }
}

public class UpdatePlaceRequestBody
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
}

public class UpdatePlaceCommandValidator : AbstractValidator<UpdatePlaceCommand>
{
    public UpdatePlaceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.Name)
            .MaximumLength(40);
        RuleFor(x => x.Address)
            .MaximumLength(100);
    }
}

public class UpdatePlaceRequestHandler : IRequestHandler<UpdatePlaceCommand, PlaceDto>
{
    private readonly IPlaceRepository<Domain.Place> _repository;
    private readonly IMapper _mapper;

    public UpdatePlaceRequestHandler(IPlaceRepository<Domain.Place> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PlaceDto> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var newPlace =_mapper.Map(request, entity);
        var result = await _repository.UpdateAsync(newPlace, cancellationToken);
        return _mapper.Map<Domain.Place, PlaceDto>(result);
    }
}
