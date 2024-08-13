using AutoMapper;
using Common.Interfaces.IRepositories.Table;
using Common.Mappings;
using FluentValidation;
using MediatR;

namespace Table.Features.Place;

public class CreatePlaceCommand : IRequest<int>, IMapFrom<Domain.Place>
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Place, CreatePlaceCommand>().ReverseMap();
        }
    }
}

public class CreatePlaceCommandValidator : AbstractValidator<CreatePlaceCommand>
{
    public CreatePlaceCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(40);
        RuleFor(x => x.Address)
            .MaximumLength(100);
    }
}

internal sealed class CreatePlaceRequestHandler : IRequestHandler<CreatePlaceCommand, int>
{
    private readonly IPlaceRepository<Domain.Place> _repository;
    private readonly IMapper _mapper;

    public CreatePlaceRequestHandler(IPlaceRepository<Domain.Place> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
    {
        var newEntity = new Domain.Place();
        var entity = _mapper.Map(request, newEntity);
        return await _repository.CreateAsync(entity, cancellationToken);
    }
}