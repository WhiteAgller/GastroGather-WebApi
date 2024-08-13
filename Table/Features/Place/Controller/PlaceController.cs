using AutoMapper;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Table.Features.Place.Dtos;

namespace Table.Features.Place.Controller;

public class PlaceController : ApiControllerBase
{
    private readonly IMapper _mapper;
    
    public PlaceController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<PlaceDto>> GetPlace([FromQuery] GetPlaceQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<PaginatedList<PlaceDto>>> GetAllPlacesByUserId([FromQuery] GetAllPlacesQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CreatePlace([FromBody] CreatePlaceCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut] 
    public async Task<ActionResult<PlaceDto>> UpdatePlace([FromQuery] int id, [FromBody] UpdatePlaceRequestBody command)
    {
        var request = _mapper.Map<UpdatePlaceRequestBody, UpdatePlaceCommand>(command);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeletePlaceCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
}