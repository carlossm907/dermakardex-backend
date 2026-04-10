using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries;
using Products.Domain.Model.Queries.StockEntryProductReport;
using Products.Domain.Services;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/entries-report")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Product Entries Report Endpoints")]
public class ProductEntriesReportController(IProductQueryService productQueryService) : ControllerBase
{
    public object ProductDailyEntriesReportResourceFromEntityAssembler { get; private set; }

    [HttpGet("product")]
    public async Task<IActionResult> GetProductEntriesReport(
        [FromQuery] int productId,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var query = new GetProductEntriesReportQuery(productId, from, to);

        var result = await productQueryService.Handle(query);

        var resources = result
            .Select(ProductEntriesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("bulk")]
    public async Task<IActionResult> GetProductsEntriesReport(
        [FromQuery] IEnumerable<int> productIds,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var query = new GetProductsEntriesReportQuery(productIds, from, to);

        var result = await productQueryService.Handle(query);

        var resources = result
            .Select(ProductEntriesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllEntriesReport(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var query = new GetAllProductsEntriesReportQuery(from, to);

        var result = await productQueryService.Handle(query);

        var resources = result
            .Select(ProductEntriesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("affected")]
    [SwaggerOperation(
    "Get Affected Products Entries Report",
    "Returns the entries report only for products with entries in the selected period.",
    OperationId = "GetAffectedProductsEntriesReport")]
    [SwaggerResponse(200, "The entries report was generated.")]
    public async Task<IActionResult> GetAffectedProductsEntriesReport(
    [FromQuery] DateOnly from,
    [FromQuery] DateOnly to)
    {
        var query = new GetAffectedProductsEntriesReportQuery(from, to);

        var report = await productQueryService.Handle(query);

        var resources = report.Select(
            ProductEntriesReportResourceFromEntityAssembler.ToResourceFromEntity
        );

        return Ok(resources);
    }
}