using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries.StockEntryProductReport;
using Products.Domain.Model.ReadModels;
using Products.Domain.Services.QueryServices;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Product Entries Report Endpoints")]
public class EntriesReportController(

    IEntriesReportQueryService entriesReportQueryService

) : ControllerBase
{

    [HttpGet("{productId:int}")]
    [SwaggerOperation(
        Summary = "Get entries report for a single product",
        Description = "Returns total entries for a product in a date range",
        OperationId = "GetProductEntriesReport"
    )]
    [SwaggerResponse(200, "The entries report was generated.", typeof(IEnumerable<ProductDailyEntriesReportItem>))]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> GetProductEntriesReport(
        int productId,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var query = new GetProductEntriesReportQuery(productId, from, to);

        var result = await entriesReportQueryService.Handle(query);

        var resources = result
            .Select(ProductEntriesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("bulk")]
    [SwaggerOperation(
        Summary = "Get entries report for multiple products",
        Description = "Returns total entries for multiple products in a date range",
        OperationId = "GetProductsEntriesReport"
    )]
    [SwaggerResponse(200, "The entries report was generated.", typeof(IEnumerable<ProductDailyEntriesReportItem>))]
    [SwaggerResponse(400, "Invalid product IDs provided.")]
    public async Task<IActionResult> GetProductsEntriesReport(
        [FromQuery] IEnumerable<int> productIds,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var query = new GetProductsEntriesReportQuery(productIds, from, to);

        var result = await entriesReportQueryService.Handle(query);

        var resources = result
            .Select(ProductEntriesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("all")]
    [SwaggerOperation(
        Summary = "Get entries report for all products",
        Description = "Returns total entries for all products in a date range",
        OperationId = "GetAllEntriesReport"
    )]
    [SwaggerResponse(200, "The entries report was generated.", typeof(IEnumerable<ProductDailyEntriesReportItem>))]
    public async Task<IActionResult> GetAllEntriesReport(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var query = new GetAllProductsEntriesReportQuery(from, to);

        var result = await entriesReportQueryService.Handle(query);

        var resources = result
            .Select(ProductEntriesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("affected")]
    [SwaggerOperation(
        Summary = "Get Affected Products Entries Report",
        Description = "Returns the entries report only for products with entries in the selected period.",
        OperationId = "GetAffectedProductsEntriesReport"
    )]
    [SwaggerResponse(200, "The entries report was generated.", typeof(IEnumerable<ProductDailyEntriesReportItem>))]
    public async Task<IActionResult> GetAffectedProductsEntriesReport(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var query = new GetAffectedProductsEntriesReportQuery(from, to);

        var report = await entriesReportQueryService.Handle(query);

        var resources = report.Select(
            ProductEntriesReportResourceFromEntityAssembler.ToResourceFromEntity
        );

        return Ok(resources);
    }
}