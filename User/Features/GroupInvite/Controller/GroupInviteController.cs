using AutoMapper;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using User.Features.GroupInvite.Dtos;
using User.Features.Invite;

namespace User.Features.GroupInvite.Controller;

public class GroupInviteController : ApiControllerBase
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IMapper _mapper;
    
    public GroupInviteController(IOpenIddictApplicationManager applicationManager, IMapper mapper)
    {
        _applicationManager = applicationManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<GroupInviteDto>> GetInvite([FromQuery] GetInviteQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("GetAllInvitesByUserId")]
    public async Task<ActionResult<PaginatedList<GroupInviteDto>>> GetAllInvitesByUserOrderId([FromQuery] GetAllInvitesByUserIdQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CreateInvite([FromBody]CreateInviteCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut] 
    public async Task<ActionResult<GroupInviteDto>> UpdateInvite([FromQuery] int id, [FromBody] UpdateInviteRequestBody command)
    {
        var request = _mapper.Map<UpdateInviteRequestBody, UpdateInviteCommand>(command);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpPut("Accept")]
    public async Task<ActionResult<GroupInviteDto>> AcceptGroupInvite([FromQuery] AcceptGroupInviteCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut("Decline")]
    public async Task<ActionResult<GroupInviteDto>> DeclineGroupInvite([FromQuery] DeclineGroupInviteCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeleteInviteCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
    
    
}