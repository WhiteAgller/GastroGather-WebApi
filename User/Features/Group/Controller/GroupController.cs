using AutoMapper;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using User.Features.Group.Dtos;

namespace User.Features.Group.Controller;

public class GroupController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public GroupController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateGroupCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<ActionResult<GroupDto>> Get([FromQuery] GetGroupQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("byUserId")]
    public async Task<ActionResult<PaginatedList<GroupDto>>> GetByUserId([FromQuery] GetGroupsByUserIdQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPut] 
    public async Task<ActionResult<GroupDto>> UpdateTable([FromQuery] int id, [FromBody] UpdateGroupRequestBody command)
    {
        var request = _mapper.Map<UpdateGroupRequestBody, UpdateGroupCommand>(command);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeleteGroupCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
    
}