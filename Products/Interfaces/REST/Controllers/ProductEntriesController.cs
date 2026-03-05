using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Services;
using Products.Interfaces.REST.Resources;
using Products.Interfaces.REST.Transform;

namespace Products.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/products/{productId:int}/entries")]
public class ProductEntriesController : ControllerBase
{
    private readonly IProductCommandService productCommandService;

    public ProductEntriesController(IProductCommandService productCommandService)
    {
        this.productCommandService = productCommandService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEntry(int productId, [FromBody] CreateStockEntryResource resource)
    {

        var command = RegisterProductEntryCommandFromResourceAssembler.ToCommandFromResource(productId, resource);

        await productCommandService.Handle(command);

        return NoContent();
    }
}