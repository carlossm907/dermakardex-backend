using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Sales.Domain.Model.Queries;
using Sales.Domain.Services;
using Sales.Interfaces.REST.Resources;
using Sales.Interfaces.REST.Resources.SalesTimeLine;
using Sales.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Sales.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/sales")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Sales by Seller Endpoints.")]
public class SaleBySellerController(
    ISaleBySellerQueryService saleBySellerQueryService
) : ControllerBase

{
    [HttpGet("by-seller/{userId:int}")]
    [SwaggerOperation("Get Sales by Seller", "Get all sales for a specific seller.", OperationId = "GetSalesBySeller")]
    [SwaggerResponse(200, "The sales were found and returned.", typeof(IEnumerable<SaleResource>))]
    public async Task<IActionResult> GetSalesBySeller(int userId)
    {
        var getSalesBySellerUserIdQuery = new GetSalesBySellerUserIdQuery(userId);
        var sales = await saleBySellerQueryService.Handle(getSalesBySellerUserIdQuery);

        var salesResource = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(salesResource);
    }

    [HttpGet("report/month/timeline/{year:int}/{month:int}/timeline")]
    [SwaggerOperation(
    Summary = "Get Sales Timeline By Month",
    Description = "Returns sales grouped by day with sequential seller blocks.",
    OperationId = "GetSalesTimelineByMonth")]
    [SwaggerResponse(200, "Sales timeline retrieved successfully.", typeof(IEnumerable<SalesTimelineByDayResource>))]
    public async Task<IActionResult> GetSalesTimelineByMonth(int year, int month)
    {
        var query = new GetSalesTimelineByMonthQuery(year, month);

        var result = await saleBySellerQueryService.Handle(query);

        var resources = result
            .Select(SalesTimelineResourceFromModelAssembler.ToResourceFromModel);

        return Ok(resources);
    }
}