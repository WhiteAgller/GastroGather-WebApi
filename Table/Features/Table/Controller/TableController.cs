using AutoMapper;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Table.Features.Table.Dtos;

namespace Table.Features.Table.Controller;

public class TableController : ApiControllerBase
{
    private readonly IMapper _mapper;
    
    public TableController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<TableDto>> GetTable([FromQuery] GetTableQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("AllByGroupId")]
    public async Task<ActionResult<PaginatedList<TableDto>>> GetAllTablesByGroupId([FromQuery] GetAllTablesByGroupIdQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("AllByUserId")]
    public async Task<ActionResult<PaginatedList<TableDto>>> GetAllTablesByUserId([FromQuery] GetAllTablesByUserIdQuery query)
    {
        return await Mediator.Send(query);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<int>> CreateTable([FromBody] CreateTableCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut] 
    public async Task<ActionResult<TableDto>> UpdateTable([FromQuery] int id, [FromBody] UpdateTableRequestBody command)
    {
        var request = _mapper.Map<UpdateTableRequestBody, UpdateTableCommand>(command);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeleteTableCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
}