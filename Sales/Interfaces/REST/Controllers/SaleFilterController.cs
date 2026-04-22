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
[SwaggerTag("Sales Filter Endpoints.")]
public class SaleFilterController(
    ISaleFilterQueryService saleFilterQueryService
) : ControllerBase
{
    [HttpGet("day/{date}")]
    [SwaggerOperation("Get Sales by Day", "Get all sales for a specific day.", OperationId = "GetSalesByDay")]
    [SwaggerResponse(200, "The sales were found and returned.", typeof(IEnumerable<SaleResource>))]
    public async Task<IActionResult> GetSalesByDay(DateOnly date)
    {
        var query = new GetSalesByDayQuery(date);
        var sales = await saleFilterQueryService.Handle(query);

        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("month/{year:int}/{month:int}")]
    [SwaggerOperation("Get Sales by Month", "Get all sales for a specific month.", OperationId = "GetSalesByMonth")]
    [SwaggerResponse(200, "The sales were found and returned.", typeof(IEnumerable<SaleResource>))]
    public async Task<IActionResult> GetSalesByMonth(int year, int month)
    {
        var query = new GetSalesByMonthQuery(year, month);
        var sales = await saleFilterQueryService.Handle(query);

        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("customer/{dni}")]
    [SwaggerOperation("Get Sales by Customer", "Get all sales for a specific customer by DNI.", OperationId = "GetSalesByCustomer")]
    [SwaggerResponse(200, "The sales were found and returned.", typeof(IEnumerable<SaleResource>))]
    public async Task<IActionResult> GetSaleByCustomer(string dni)
    {
        var getSalesByCustomerDniQuery = new GetSalesByCustomerDniQuery(dni);
        var sales = await saleFilterQueryService.Handle(getSalesByCustomerDniQuery);

        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

}