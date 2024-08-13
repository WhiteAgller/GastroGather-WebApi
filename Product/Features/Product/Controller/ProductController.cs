using AutoMapper;
using Common;
using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using Product.Features.Product.Dtos;

namespace Product.Features.Product.Controller;
 
public class ProductController : ApiControllerBase
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;
    
    public ProductController(IOpenIddictApplicationManager applicationManager, IMapper mapper, ICurrentUserService currentUser)
    {
        _applicationManager = applicationManager;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    
    [HttpGet]
    public async Task<ActionResult<ProductDto>> Get([FromQuery] GetProductQuery query)
    {
        return await Mediator.Send(query);
    } 
    
    [HttpGet("all")]
    public Task<PaginatedList<ProductDto>> GetProducts([FromQuery] GetProductsQuery query)
    {
        return Mediator.Send(query);
    }
    
    [HttpGet("allByUserId")]
    public Task<PaginatedList<ProductDto>> GetProducts([FromQuery] GetProductsByUserIdQuery query)
    {
        return Mediator.Send(query);
    }
    
    //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
    {
        // var subject = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        // if (string.IsNullOrEmpty(subject))
        // {
        //     return BadRequest();
        // }
        //
        // var application = await _applicationManager.FindByClientIdAsync(subject);
        // if (application == null)
        // {
        //     return BadRequest();
        // }
        
        return await Mediator.Send(command);
    }
    
    [HttpPut] 
    public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromBody] UpdateProductRequestBody body)
    {
        var request = _mapper.Map<UpdateProductRequestBody, UpdateProductCommand>(body);
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Task>> Delete([FromQuery] DeleteProductCommand commnad)
    {
        return await Mediator.Send(commnad);
    } 
}