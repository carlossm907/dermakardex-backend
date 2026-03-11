using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Commands.ProductDiscount;
using Products.Domain.Model.Queries;
using Products.Domain.Services;
using Products.Interfaces.REST.Resources;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/product-discounts")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Products Schedules Discounts Endpoints.")]

public class ProductDiscountsController(
    IProductDiscountCommandService productDiscountCommandService,
    IProductDiscountQueryService productDiscountQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scheduled discounts",
        Description = "Returns all scheduled product discounts",
        OperationId = "GetAllScheduledDiscounts"
    )]
    public async Task<IActionResult> GetAllScheduledDiscounts()
    {
        var query = new GetAllScheduledDiscountsQuery();

        var discounts = await productDiscountQueryService.Handle(query);

        var resources = discounts.Select(ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("active")]
    [SwaggerOperation(
        Summary = "Get active scheduled discounts",
        Description = "Returns currently active product discounts",
        OperationId = "GetActiveScheduledDiscounts"
    )]
    public async Task<IActionResult> GetActiveScheduledDiscounts()
    {
        var query = new GetActiveScheduledDiscountsQuery();

        var discounts = await productDiscountQueryService.Handle(query);

        var resources = discounts.Select(ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("product/{productId:int}")]
    [SwaggerOperation(
        Summary = "Get scheduled discounts by product",
        Description = "Returns all scheduled discounts for a specific product",
        OperationId = "GetScheduledDiscountsByProductId"
    )]
    public async Task<IActionResult> GetScheduledDiscountsByProductId(int productId)
    {
        var query = new GetScheduledDiscountsByProductIdQuery(productId);

        var discounts = await productDiscountQueryService.Handle(query);

        var resources = discounts.Select(ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("products")]
    [SwaggerOperation(
    Summary = "Get products with scheduled discounts",
    Description = "Returns products that have scheduled discounts",
    OperationId = "GetProductsWithScheduledDiscounts"
    )]
    public async Task<IActionResult> GetProductsWithScheduledDiscounts()
    {
        var query = new GetProductsWithScheduledDiscountsQuery();

        var products = await productDiscountQueryService.Handle(query);

        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("expired")]
    [SwaggerOperation(
    Summary = "Get expired scheduled discounts",
    Description = "Returns expired scheduled product discounts",
    OperationId = "GetExpiredScheduledDiscounts"
    )]
    public async Task<IActionResult> GetExpiredScheduledDiscounts()
    {
        var query = new GetExpiredScheduledDiscountsQuery();

        var discounts = await productDiscountQueryService.Handle(query);

        var resources = discounts.Select(ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Schedule discount to product",
        Description = "Creates a scheduled discount for a product",
        OperationId = "ScheduleDiscountToProduct"
    )]
    public async Task<IActionResult> ScheduleDiscount([FromBody] ScheduleDiscountResource resource)
    {
        var command = ScheduleDiscountCommandFromResourceAssembler.ToCommandFromResource(resource);

        var discount = await productDiscountCommandService.Handle(command);

        var response = ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity(discount);

        return CreatedAtAction(nameof(GetAllScheduledDiscounts), response);
    }

    [HttpPost("bulk")]
    [SwaggerOperation(
    Summary = "Schedule discount to multiple products",
    Description = "Creates a scheduled discount for multiple products",
    OperationId = "ScheduleDiscountToProducts"
    )]
    public async Task<IActionResult> ScheduleDiscountToProducts([FromBody] ScheduleDiscountToProductsResource resource)
    {
        var command = ScheduleDiscountToProductsCommandFromResourceAssembler.ToCommandFromResource(resource);

        await productDiscountCommandService.Handle(command);

        return Ok();
    }

    [HttpPost("all")]
    [SwaggerOperation(
    Summary = "Schedule discount to all products",
    Description = "Creates a scheduled discount for all products",
    OperationId = "ScheduleDiscountToAllProducts"
    )]
    public async Task<IActionResult> ScheduleDiscountToAllProducts([FromBody] ScheduleDiscountToAllProductsResource resource)
    {
        var command = ScheduleDiscountToAllProductsCommandFromResourceAssembler.ToCommandFromResource(resource);

        await productDiscountCommandService.Handle(command);

        return Ok();
    }

    [HttpPost("cleanup-expired")]
    [SwaggerOperation(
    Summary = "Cleanup expired scheduled discounts",
    Description = "Disables expired scheduled discounts",
    OperationId = "CleanupExpiredScheduledDiscounts"
    )]
    public async Task<IActionResult> CleanupExpiredDiscounts()
    {
        var command = new CleanupExpiredProductDiscountsCommand();

        await productDiscountCommandService.Handle(command);

        return NoContent();
    }

    [HttpPut("{discountId:int}")]
    [SwaggerOperation(
        Summary = "Update scheduled discount",
        Description = "Updates a scheduled discount",
        OperationId = "UpdateScheduledDiscount"
    )]
    public async Task<IActionResult> UpdateScheduledDiscount(int discountId, [FromBody] UpdateScheduledDiscountResource resource
    )
    {
        var command = UpdateScheduledDiscountCommandFromResourceAssembler.ToCommandFromResource(discountId, resource);

        await productDiscountCommandService.Handle(command);

        return NoContent();
    }

    [HttpDelete("{discountId:int}")]
    [SwaggerOperation(
        Summary = "Delete scheduled discount",
        Description = "Deletes a scheduled discount",
        OperationId = "DeleteScheduledDiscount"
    )]
    public async Task<IActionResult> DeleteScheduledDiscount(int discountId)
    {
        var command = new DeleteScheduledDiscountCommand(discountId);

        await productDiscountCommandService.Handle(command);

        return NoContent();
    }

    [HttpPatch("{discountId:int}/disable")]
    [SwaggerOperation(
        Summary = "Disable scheduled discount",
        Description = "Disables a scheduled discount",
        OperationId = "DisableScheduledDiscount"
    )]
    public async Task<IActionResult> DisableScheduledDiscount(int discountId)
    {
        var command = new DisableScheduledDiscountCommand(discountId);

        await productDiscountCommandService.Handle(command);

        return NoContent();
    }

}