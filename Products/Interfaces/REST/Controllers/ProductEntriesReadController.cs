using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries;
using Products.Domain.Services;
using Products.Interfaces.REST.Transform;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/products/{productId:int}/entries")]
public class ProductEntriesReadController : ControllerBase
{
    private readonly IStockEntryQueryService stockEntryQueryService;

    public ProductEntriesReadController(IStockEntryQueryService stockEntryQueryService)
    {
        this.stockEntryQueryService = stockEntryQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        var query = new GetProductStockEntriesQuery(productId);
        var entries = await stockEntryQueryService.Handle(query);

        var resources = entries.Select(e =>
            StockEntryResourceFromEntityAssembler.ToResourceFromEntity(e, string.Empty));

        return Ok(resources);
    }
}