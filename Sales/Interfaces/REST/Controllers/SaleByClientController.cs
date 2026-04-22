using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Sales.Domain.Model.Queries;
using Sales.Domain.Services;
using Sales.Interfaces.REST.Resources;
using Sales.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Sales.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/sales")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Sales by Client Endpoints.")]
public class SaleByClientController(

    ISaleByClientQueryService saleByClientQueryService

) : ControllerBase

{
    [HttpGet("report/day/{date}")]
    [SwaggerOperation(
    Summary = "Get Sales Report By Day",
    Description = "Returns all sales grouped by customer for a specific day.",
    OperationId = "GetSalesReportByDay")]
    [SwaggerResponse(200, "Sales report retrieved successfully.",
    typeof(IEnumerable<SalesGroupedByCustomerReportResource>))]
    public async Task<IActionResult> GetSalesReportByDay(DateOnly date)
    {
        var query = new GetSalesGroupedByCustomerByDayQuery(date);

        var report = await saleByClientQueryService.Handle(query);

        var resources = report
            .Select(SalesGroupedByCustomerReportResourceFromModelAssembler.ToResourceFromModel);

        return Ok(resources);
    }

    [HttpGet("report/month/{year:int}/{month:int}")]
    [SwaggerOperation(
    Summary = "Get Sales Report By Month",
    Description = "Returns all sales grouped by customer for a specific month.",
    OperationId = "GetSalesReportByMonth")]
    [SwaggerResponse(200, "Sales report retrieved successfully.",
    typeof(IEnumerable<SalesGroupedByCustomerReportResource>))]
    public async Task<IActionResult> GetSalesReportByMonth(int year, int month)
    {
        var query = new GetSalesGroupedByCustomerByMonthQuery(year, month);

        var report = await saleByClientQueryService.Handle(query);

        var resources = report
            .Select(SalesGroupedByCustomerReportResourceFromModelAssembler.ToResourceFromModel);

        return Ok(resources);
    }
}