using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using User.Features.User.Dtos;

namespace User.Features.User.Controller;

public class UserController : ApiControllerBase
{
    [HttpGet("GetAllFriends")]
    public async Task<ActionResult<PaginatedList<UserDto>>> GetAllFriends([FromQuery] GetAllFriendsQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("GetAllFriendInvites")]
    public async Task<ActionResult<PaginatedList<UserDto>>> GetAllFriendInvites([FromQuery] GetAllFriendInvitesQuery query)
    {
        return await Mediator.Send(query);
    }
}