using AutoMapper;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using User.Features.Invite.Dtos;

namespace User.Features.Invite.Controller;

public class InviteController : ApiControllerBase
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IMapper _mapper;
    
    public InviteController(IOpenIddictApplicationManager applicationManager, IMapper mapper)
    {
        _applicationManager = applicationManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<InviteDto>> GetInvite([FromQuery] GetInviteQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("GetAllInvitesByUserId")]
    public async Task<ActionResult<PaginatedList<InviteDto>>> GetAllInvitesByUserOrderId([FromQuery] GetAllInvitesByUserIdQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CreateInvite([FromBody]CreateInviteCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut] 
    public async Task<ActionResult<InviteDto>> UpdateInvite([FromQuery] int id, [FromBody] UpdateInviteRequestBody command)
    {
        var request = _mapper.Map<UpdateInviteRequestBody, UpdateInviteCommand>(command);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeleteInviteCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
}