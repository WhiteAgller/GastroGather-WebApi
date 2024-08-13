using AutoMapper;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using Order.Features.OrderItem.Dtos;

namespace Order.Features.OrderItem.Controller;

public class OrderItemController : ApiControllerBase
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IMapper _mapper;
    
    public OrderItemController(IOpenIddictApplicationManager applicationManager, IMapper mapper)
    {
        _applicationManager = applicationManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<OrderItemDto>> GetOrderItem(GetOrderItemQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("GetAllOrderItemsByUserOrderId")]
    public async Task<ActionResult<PaginatedList<OrderItemDto>>> GetAllOrderItemsByUserOrderId(GetAllOrderItemsByOrderIdQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CreateOrderItem(CreateOrderItemCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut] 
    public async Task<ActionResult<OrderItemDto>> UpdateOrderItem([FromQuery] int id, [FromBody] UpdateOrderItemRequestBody command)
    {
        var request = _mapper.Map<UpdateOrderItemRequestBody, UpdateOrderItemCommand>(command);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeleteOrderItemCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
}