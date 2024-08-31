using AutoMapper;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using User.Features.FriendInvite.Dtos;

namespace User.Features.FriendInvite.Controller;

public class FriendInviteController : ApiControllerBase
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IMapper _mapper;
    
    public FriendInviteController(IOpenIddictApplicationManager applicationManager, IMapper mapper)
    {
        _applicationManager = applicationManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<FriendInviteDto>> GetFriendInvite([FromQuery] GetFriendInviteQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CreateFriendInvite([FromBody]CreateFriendInviteCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("Accept")]
    public async Task<ActionResult<FriendInviteDto>> AcceptFriendInvite([FromQuery] AcceptFriendInviteCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut("Decline")]
    public async Task<ActionResult<FriendInviteDto>> DeclineFriendInvite([FromQuery] DeclineFriendInviteCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut] 
    public async Task<ActionResult<FriendInviteDto>> UpdateFriendInvite([FromQuery] int id, [FromBody] UpdateFriendInviteRequestBody command)
    {
        var request = _mapper.Map<UpdateFriendInviteRequestBody, UpdateFriendInviteCommand>(command);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeleteFriendInviteCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
}