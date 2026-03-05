using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries;
using Products.Domain.Services;
using Products.Interfaces.REST.Transform;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/stock-entries")]
public class StockEntriesController : ControllerBase
{
    private readonly IStockEntryQueryService stockEntryQueryService;

    public StockEntriesController(IStockEntryQueryService stockEntryQueryService)
    {
        this.stockEntryQueryService = stockEntryQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllStockEntriesQuery();

        var entries = await stockEntryQueryService.Handle(query);

        var resources = entries.Select(e =>
            StockEntryResourceFromEntityAssembler.ToResourceFromEntity(e));

        return Ok(resources);

    }
}