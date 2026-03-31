using Microsoft.AspNetCore.Mvc;
using Sales.Domain.Model.Queries;
using Sales.Domain.Services;
using Sales.Interfaces.REST.Resources;
using Sales.Interfaces.REST.Resources.SalesTimeLine;
using Sales.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Sales.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SalesController(ISaleCommandService saleCommandService, ISaleQueryService saleQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllSales()
    {
        var getAllSalesQuery = new GetAllSalesQuery();
        var sales = await saleQueryService.Handle(getAllSalesQuery);
        var saleResources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(saleResources);

    }

    [HttpGet("{saleId:int}")]
    public async Task<IActionResult> GetSaleById(int saleId)
    {
        var getSaleByIdQuery = new GetSaleByIdQuery(saleId);
        var sale = await saleQueryService.Handle(getSaleByIdQuery);
        if (sale is null) return NotFound();
        var saleResource = SaleDetailResourceFromEntityAssembler.ToResourceFromEntity(sale);

        return Ok(saleResource);
    }

    [HttpGet("customer/{dni}")]
    public async Task<IActionResult> GetSaleByCustomer(string dni)
    {
        var getSalesByCustomerDniQuery = new GetSalesByCustomerDniQuery(dni);
        var sales = await saleQueryService.Handle(getSalesByCustomerDniQuery);

        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("seller/{userId:int}")]
    public async Task<IActionResult> GetSalesBySeller(int userId)
    {
        var getSalesBySellerUserIdQuery = new GetSalesBySellerUserIdQuery(userId);
        var sales = await saleQueryService.Handle(getSalesBySellerUserIdQuery);

        var salesResource = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(salesResource);
    }

    [HttpGet("product/{productId:int}")]
    public async Task<IActionResult> GetSalesByProduct(int productId)
    {
        var query = new GetSalesByProductIdQuery(productId);
        var sales = await saleQueryService.Handle(query);

        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("day/{date}")]
    public async Task<IActionResult> GetSalesByDay(DateOnly date)
    {
        var query = new GetSalesByDayQuery(date);
        var sales = await saleQueryService.Handle(query);

        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("month/{year:int}/{month:int}")]
    public async Task<IActionResult> GetSalesByMonth(int year, int month)
    {
        var query = new GetSalesByMonthQuery(year, month);
        var sales = await saleQueryService.Handle(query);

        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

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

        var report = await saleQueryService.Handle(query);

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

        var report = await saleQueryService.Handle(query);

        var resources = report
            .Select(SalesGroupedByCustomerReportResourceFromModelAssembler.ToResourceFromModel);

        return Ok(resources);
    }

    [HttpGet("report/month/timeline/{year:int}/{month:int}")]
    [SwaggerOperation(
    Summary = "Get Sales Timeline By Month",
    Description = "Returns sales grouped by day with sequential seller blocks.",
    OperationId = "GetSalesTimelineByMonth")]
    [SwaggerResponse(200, "Sales timeline retrieved successfully.",
    typeof(IEnumerable<SalesTimelineByDayResource>))]
    public async Task<IActionResult> GetSalesTimelineByMonth(int year, int month)
    {
        var query = new GetSalesTimelineByMonthQuery(year, month);

        var result = await saleQueryService.Handle(query);

        var resources = result
            .Select(SalesTimelineResourceFromModelAssembler.ToResourceFromModel);

        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterSale(RegisterSaleResource resource)
    {
        var registerSaleCommand = RegisterSaleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var sale = await saleCommandService.Handle(registerSaleCommand);
        if (sale is null) return BadRequest();

        var saleResource = SaleDetailResourceFromEntityAssembler.ToResourceFromEntity(sale);
        return CreatedAtAction(nameof(GetSaleById), new { saleId = sale.Id }, saleResource);
    }
}