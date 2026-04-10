using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries.SalesProductReport;
using Products.Domain.Services;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/sales-report")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Product Sales Report Endpoints")]
public class ProductSalesReportController(IProductQueryService productQueryService) : ControllerBase
{

    [HttpGet("product")]
    [SwaggerOperation(
        Summary = "Get sales report for a single product",
        Description = "Returns total sales for a product in a date range"
    )]
    public async Task<IActionResult> GetProductSalesReport(
        [FromQuery] int productId,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to
    )
    {
        var query = new GetProductSalesReportQuery(productId, from, to);

        var result = await productQueryService.Handle(query);

        var resources = result
            .Select(ProductSalesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("bulk")]
    [SwaggerOperation(
        Summary = "Get sales report for multiple products",
        Description = "Returns total sales for multiple products in a date range"
    )]
    public async Task<IActionResult> GetProductsSalesReport(
        [FromQuery] IEnumerable<int> productIds,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to
    )
    {
        var query = new GetProductsSalesReportQuery(productIds, from, to);

        var result = await productQueryService.Handle(query);

        var resources = result
            .Select(ProductSalesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("all")]
    [SwaggerOperation(
        Summary = "Get sales report for all products",
        Description = "Returns total sales for all products in a date range"
    )]
    public async Task<IActionResult> GetAllProductsSalesReport(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to
    )
    {
        var query = new GetAllProductsSalesReportQuery(from, to);

        var result = await productQueryService.Handle(query);

        var resources = result
            .Select(ProductSalesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("affected")]
    [SwaggerOperation(
    "Get Affected Products Sales Report",
    "Returns the sales report only for products with sales in the selected period.",
    OperationId = "GetAffectedProductsSalesReport")]
    public async Task<IActionResult> GetAffectedProductsSalesReport(
    [FromQuery] DateOnly from,
    [FromQuery] DateOnly to)
    {
        var query = new GetAffectedProductsSalesReportQuery(from, to);

        var report = await productQueryService.Handle(query);

        var resources = report.Select(
            ProductSalesReportResourceFromEntityAssembler.ToResourceFromEntity
        );

        return Ok(resources);
    }

}