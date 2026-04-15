using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries.SalesProductReport;
using Products.Domain.Model.ReadModels;
using Products.Domain.Services.QueryServices;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Sales Report Endpoints")]
public class SalesReportController(
    ISalesReportQueryService salesReportQueryService) : ControllerBase
{

    [HttpGet("{productId:int}")]
    [SwaggerOperation(
        Summary = "Get sales report for a single product",
        Description = "Returns total sales for a product in a date range",
        OperationId = "GetProductSalesReport"
    )]
    [SwaggerResponse(200, "The sales report was generated.", typeof(IEnumerable<ProductSalesReportItem>))]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> GetProductSalesReport(
        int productId,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to
    )
    {
        var query = new GetProductSalesReportQuery(productId, from, to);

        var result = await salesReportQueryService.Handle(query);

        var resources = result
            .Select(ProductSalesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("bulk")]
    [SwaggerOperation(
        Summary = "Get sales report for multiple products",
        Description = "Returns total sales for multiple products in a date range",
        OperationId = "GetProductsSalesReport"
    )]
    [SwaggerResponse(200, "The sales report was generated.", typeof(IEnumerable<ProductSalesReportItem>))]
    [SwaggerResponse(400, "Invalid product IDs provided.")]
    public async Task<IActionResult> GetProductsSalesReport(
        [FromQuery] IEnumerable<int> productIds,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to
    )
    {
        var query = new GetProductsSalesReportQuery(productIds, from, to);

        var result = await salesReportQueryService.Handle(query);

        var resources = result
            .Select(ProductSalesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("all")]
    [SwaggerOperation(
        Summary = "Get sales report for all products",
        Description = "Returns total sales for all products in a date range",
        OperationId = "GetAllProductsSalesReport"
    )]
    [SwaggerResponse(200, "The sales report was generated.", typeof(IEnumerable<ProductSalesReportItem>))]
    public async Task<IActionResult> GetAllProductsSalesReport(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to
    )
    {
        var query = new GetAllProductsSalesReportQuery(from, to);

        var result = await salesReportQueryService.Handle(query);

        var resources = result
            .Select(ProductSalesReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("affected")]
    [SwaggerOperation(
        Summary = "Get Affected Products Sales Report",
        Description = "Returns the sales report only for products with sales in the selected period.",
        OperationId = "GetAffectedProductsSalesReport"
    )]
    [SwaggerResponse(200, "The sales report was generated.", typeof(IEnumerable<ProductSalesReportItem>))]
    public async Task<IActionResult> GetAffectedProductsSalesReport(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to
    )
    {
        var query = new GetAffectedProductsSalesReportQuery(from, to);

        var report = await salesReportQueryService.Handle(query);

        var resources = report.Select(
            ProductSalesReportResourceFromEntityAssembler.ToResourceFromEntity
        );

        return Ok(resources);
    }

}