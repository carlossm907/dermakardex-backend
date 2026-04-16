using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries.StockEntry;
using Products.Domain.Services;
using Products.Interfaces.REST.Resources;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/products")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Stock Entry Endpoints.")]
public class StockEntriesController(
    IProductCommandService productCommandService,
    IStockEntryQueryService stockEntryQueryService
) : ControllerBase
{
    [HttpGet("entries/all")]
    [SwaggerOperation("Get All Stock Entries", "Get all stock entries in the system.", OperationId = "GetAllStockEntries")]
    [SwaggerResponse(200, "The stock entries were found and returned.", typeof(IEnumerable<StockEntryResource>))]
    [SwaggerResponse(404, "No stock entries were found.")]
    public async Task<IActionResult> GetAllEntries()
    {
        var query = new GetAllStockEntriesQuery();

        var entries = await stockEntryQueryService.Handle(query);

        var resources = entries.Select(e =>
            StockEntryResourceFromEntityAssembler.ToResourceFromEntity(e));

        return Ok(resources);

    }

    [HttpGet("{productId:int}/entries")]
    [SwaggerOperation("Get Stock Entries by Product", "Get all stock entries for a specific product.", OperationId = "GetProductStockEntries")]
    [SwaggerResponse(200, "The stock entries were found and returned.", typeof(IEnumerable<StockEntryResource>))]
    [SwaggerResponse(404, "No stock entries were found for the product.")]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        var query = new GetProductStockEntriesQuery(productId);
        var entries = await stockEntryQueryService.Handle(query);

        var resources = entries.Select(e =>
            StockEntryResourceFromEntityAssembler.ToResourceFromEntity(e));

        return Ok(resources);
    }

    [HttpPost("{productId:int}/entries")]
    [SwaggerOperation("Register Stock Entry", "Register a new stock entry for a product.", OperationId = "RegisterStockEntry")]
    [SwaggerResponse(204, "The stock entry was registered successfully.")]
    [SwaggerResponse(400, "The stock entry was not registered.")]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> RegisterEntry(int productId, [FromBody] CreateStockEntryResource resource)
    {
        var command = RegisterProductEntryCommandFromResourceAssembler.ToCommandFromResource(productId, resource);

        await productCommandService.Handle(command);

        return NoContent();
    }

}