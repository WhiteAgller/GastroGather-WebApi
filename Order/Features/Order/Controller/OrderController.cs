using AutoMapper;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Order.Features.Order.Dtos;

namespace Order.Features.Order.Controller;

public class OrderController : ApiControllerBase
{
    private readonly IMapper _mapper;
    
    public OrderController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<OrderDto>> GetOrder([FromQuery] GetOrderQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("GetOrdersByUserId")]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetAllOrdersByUserId([FromQuery] GetOrdersByUserIdQurey query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("GetOrdersByTableId")]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetAllOrdersByTableId([FromQuery] GetOrdersByUserIdQurey query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut] 
    public async Task<ActionResult<OrderDto>> UpdateOrder([FromQuery] int id, [FromBody] UpdateOrderRequestBody command)
    {
        var request = _mapper.Map<UpdateOrderRequestBody, UpdateOrderCommand>(command);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeleteOrderCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
}