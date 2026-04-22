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
[SwaggerTag("Sales Endpoints.")]
public class SalesController(ISaleCommandService saleCommandService, ISaleQueryService saleQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Sales", "Get all sales.", OperationId = "GetAllSales")]
    [SwaggerResponse(200, "The sales were found and returned.", typeof(IEnumerable<SaleResource>))]
    public async Task<IActionResult> GetAllSales()
    {
        var getAllSalesQuery = new GetAllSalesQuery();
        var sales = await saleQueryService.Handle(getAllSalesQuery);
        var saleResources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(saleResources);

    }

    [HttpGet("{saleId:int}")]
    [SwaggerOperation("Get Sale by Id", "Get a sale by its unique identifier.", OperationId = "GetSaleById")]
    [SwaggerResponse(200, "The sale was found and returned.", typeof(SaleDetailResource))]
    [SwaggerResponse(404, "The sale was not found.")]
    public async Task<IActionResult> GetSaleById(int saleId)
    {
        var getSaleByIdQuery = new GetSaleByIdQuery(saleId);
        var sale = await saleQueryService.Handle(getSaleByIdQuery);
        if (sale is null) return NotFound();
        var saleResource = SaleDetailResourceFromEntityAssembler.ToResourceFromEntity(sale);

        return Ok(saleResource);
    }

    [HttpGet("by-product/{productId:int}")]
    [SwaggerOperation("Get Sales by Product", "Get all sales for a specific product.", OperationId = "GetSalesByProduct")]
    [SwaggerResponse(200, "The sales were found and returned.", typeof(IEnumerable<SaleResource>))]
    public async Task<IActionResult> GetSalesByProduct(int productId)
    {
        var query = new GetSalesByProductIdQuery(productId);
        var sales = await saleQueryService.Handle(query);

        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation("Create Sale", "Create a new sale.", OperationId = "CreateSale")]
    [SwaggerResponse(201, "The sale was created.", typeof(SaleDetailResource))]
    [SwaggerResponse(400, "The sale was not created.")]
    public async Task<IActionResult> RegisterSale(RegisterSaleResource resource)
    {
        var registerSaleCommand = RegisterSaleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var sale = await saleCommandService.Handle(registerSaleCommand);
        if (sale is null) return BadRequest();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var saleResource = SaleDetailResourceFromEntityAssembler.ToResourceFromEntity(sale);
        return CreatedAtAction(nameof(GetSaleById), new { saleId = sale.Id }, saleResource);
    }
}