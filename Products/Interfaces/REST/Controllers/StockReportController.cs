using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries.StockReport;
using Products.Domain.Services.QueryServices;
using Products.Interfaces.REST.Resources.Reports;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Products Endpoints.")]
public class StockReportController(

    IStockReportQueryService stockReportQueryService

) : ControllerBase
{
    [HttpGet("{productId:int}/stock-report")]
    [SwaggerOperation(
    "Get Product Daily Stock Report",
    "Returns the daily stock report for a product between two dates.",
    OperationId = "GetProductDailyStockReport")]
    [SwaggerResponse(200, "The stock report was generated.", typeof(IEnumerable<ProductDailyStockReportResource>))]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> GetProductDailyStockReport(
    int productId,
    [FromQuery] DateOnly from,
    [FromQuery] DateOnly to)
    {
        var query = new GetProductDailyStockReportQuery(productId, from, to);

        var report = await stockReportQueryService.Handle(query);

        var resources = report.Select(
            ProductDailyStockReportResourceFromEntityAssembler.ToResourceFromEntity
        );

        return Ok(resources);
    }

    [HttpGet("stock-report")]
    [SwaggerOperation(
    "Get All Products Daily Stock Report",
    "Returns the daily stock report for all products between two dates.",
    OperationId = "GetAllProductsDailyStockReport")]
    [SwaggerResponse(200, "The stock report was generated.", typeof(IEnumerable<ProductDailyStockReportResource>))]
    public async Task<IActionResult> GetAllProductsDailyStockReport(
    [FromQuery] DateOnly from,
    [FromQuery] DateOnly to)
    {
        var query = new GetAllProductsDailyStockReportQuery(from, to);

        var report = await stockReportQueryService.Handle(query);

        var resources = report.Select(
            ProductDailyStockReportResourceFromEntityAssembler.ToResourceFromEntity
        );

        return Ok(resources);
    }

    [HttpGet("stock-report/affected")]
    [SwaggerOperation(
    "Get Affected Products Daily Stock Report",
    "Returns the daily stock report only for products with stock changes (entries or sales) between two dates.",
    OperationId = "GetAffectedProductsDailyStockReport")]
    [SwaggerResponse(200, "The stock report was generated.", typeof(IEnumerable<ProductDailyStockReportResource>))]
    public async Task<IActionResult> GetAffectedProductsDailyStockReport(
    [FromQuery] DateOnly from,
    [FromQuery] DateOnly to)
    {
        var query = new GetAffectedProductsDailyStockReportQuery(from, to);

        var report = await stockReportQueryService.Handle(query);

        var resources = report.Select(
            ProductDailyStockReportResourceFromEntityAssembler.ToResourceFromEntity
        );

        return Ok(resources);
    }

    [HttpGet("stock-report/bulk")]
    [SwaggerOperation(
    "Get Selected Products Daily Stock Report",
    "Returns the daily stock report for selected products between two dates.",
    OperationId = "GetProductsDailyStockReport")]
    [SwaggerResponse(200, "The stock report was generated.", typeof(IEnumerable<ProductDailyStockReportResource>))]
    public async Task<IActionResult> GetProductsDailyStockReport(
    [FromQuery] IEnumerable<int> productIds,
    [FromQuery] DateOnly from,
    [FromQuery] DateOnly to)
    {
        var query = new GetProductsDailyStockReportQuery(
            productIds,
            from,
            to
        );

        var report = await stockReportQueryService.Handle(query);

        var resources = report.Select(
            ProductDailyStockReportResourceFromEntityAssembler.ToResourceFromEntity
        );

        return Ok(resources);
    }
}