using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Commands.ScheduledDiscount;
using Products.Domain.Model.Queries.ScheduledDiscount;
using Products.Domain.Services;
using Products.Interfaces.REST.Resources;
using Products.Interfaces.REST.Resources.ScheduledDiscount;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Scheduled Discounts Endpoints.")]

public class ScheduledDiscountsController(
    IScheduledDiscountCommandService scheduledDiscountCommandService,
    IScheduledDiscountQueryService scheduledDiscountQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scheduled discounts",
        Description = "Returns all scheduled product discounts",
        OperationId = "GetAllScheduledDiscounts"
    )]
    [SwaggerResponse(200, "The scheduled discounts were found and returned.", typeof(IEnumerable<ProductDiscountResource>))]
    [SwaggerResponse(404, "No scheduled discounts were found.")]
    public async Task<IActionResult> GetAllScheduledDiscounts()
    {
        var query = new GetAllScheduledDiscountsQuery();

        var discounts = await scheduledDiscountQueryService.Handle(query);

        var resources = discounts.Select(ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("active")]
    [SwaggerOperation(
        Summary = "Get active scheduled discounts",
        Description = "Returns currently active product discounts",
        OperationId = "GetActiveScheduledDiscounts"
    )]
    [SwaggerResponse(200, "The active scheduled discounts were found and returned.", typeof(IEnumerable<ProductDiscountResource>))]
    [SwaggerResponse(404, "No active scheduled discounts were found.")]
    public async Task<IActionResult> GetActiveScheduledDiscounts()
    {
        var query = new GetActiveScheduledDiscountsQuery();

        var discounts = await scheduledDiscountQueryService.Handle(query);

        var resources = discounts.Select(ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("product/{productId:int}")]
    [SwaggerOperation(
        Summary = "Get scheduled discounts by product",
        Description = "Returns all scheduled discounts for a specific product",
        OperationId = "GetScheduledDiscountsByProductId"
    )]
    [SwaggerResponse(200, "The scheduled discounts were found and returned.", typeof(IEnumerable<ProductDiscountResource>))]
    [SwaggerResponse(404, "No scheduled discounts were found for the product.")]
    public async Task<IActionResult> GetScheduledDiscountsByProductId(int productId)
    {
        var query = new GetScheduledDiscountsByProductIdQuery(productId);

        var discounts = await scheduledDiscountQueryService.Handle(query);

        var resources = discounts.Select(ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("products")]
    [SwaggerOperation(
        Summary = "Get products with scheduled discounts",
        Description = "Returns products that have scheduled discounts",
        OperationId = "GetProductsWithScheduledDiscounts"
    )]
    [SwaggerResponse(200, "The products with scheduled discounts were found and returned.", typeof(IEnumerable<ProductResource>))]
    [SwaggerResponse(404, "No products with scheduled discounts were found.")]
    public async Task<IActionResult> GetProductsWithScheduledDiscounts()
    {
        var query = new GetProductsWithScheduledDiscountsQuery();

        var products = await scheduledDiscountQueryService.Handle(query);

        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("expired")]
    [SwaggerOperation(
        Summary = "Get expired scheduled discounts",
        Description = "Returns expired scheduled product discounts",
        OperationId = "GetExpiredScheduledDiscounts"
    )]
    [SwaggerResponse(200, "The expired scheduled discounts were found and returned.", typeof(IEnumerable<ProductDiscountResource>))]
    [SwaggerResponse(404, "No expired scheduled discounts were found.")]
    public async Task<IActionResult> GetExpiredScheduledDiscounts()
    {
        var query = new GetExpiredScheduledDiscountsQuery();

        var discounts = await scheduledDiscountQueryService.Handle(query);

        var resources = discounts.Select(ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Schedule discount to product",
        Description = "Creates a scheduled discount for a product",
        OperationId = "ScheduleDiscountToProduct"
    )]
    [SwaggerResponse(201, "The scheduled discount was created.", typeof(ProductDiscountResource))]
    [SwaggerResponse(400, "The scheduled discount was not created.")]
    public async Task<IActionResult> CreateScheduleDiscount([FromBody] ScheduleDiscountResource resource)
    {
        var command = ScheduleDiscountCommandFromResourceAssembler.ToCommandFromResource(resource);

        var discount = await scheduledDiscountCommandService.Handle(command);

        var response = ProductDiscountResourceFromEntityAssembler.ToResourceFromEntity(discount);

        return CreatedAtAction(nameof(GetAllScheduledDiscounts), response);
    }

    [HttpPost("bulk")]
    [SwaggerOperation(
        Summary = "Schedule discount to multiple products",
        Description = "Creates a scheduled discount for multiple products",
        OperationId = "ScheduleDiscountToProducts"
    )]
    [SwaggerResponse(200, "The scheduled discount was applied to all products.")]
    [SwaggerResponse(400, "The scheduled discount was not applied.")]
    public async Task<IActionResult> ScheduleDiscountToProducts([FromBody] ScheduleDiscountToProductsResource resource)
    {
        var command = ScheduleDiscountToProductsCommandFromResourceAssembler.ToCommandFromResource(resource);

        await scheduledDiscountCommandService.Handle(command);

        return Ok();
    }

    [HttpPost("all")]
    [SwaggerOperation(
        Summary = "Schedule discount to all products",
        Description = "Creates a scheduled discount for all products",
        OperationId = "ScheduleDiscountToAllProducts"
    )]
    [SwaggerResponse(200, "The scheduled discount was applied to all products.")]
    [SwaggerResponse(400, "The scheduled discount was not applied.")]
    public async Task<IActionResult> ScheduleDiscountToAllProducts([FromBody] ScheduleDiscountToAllProductsResource resource)
    {
        var command = ScheduleDiscountToAllProductsCommandFromResourceAssembler.ToCommandFromResource(resource);

        await scheduledDiscountCommandService.Handle(command);

        return Ok();
    }

    [HttpPost("cleanup-expired")]
    [SwaggerOperation(
        Summary = "Cleanup expired scheduled discounts",
        Description = "Disables expired scheduled discounts",
        OperationId = "CleanupExpiredScheduledDiscounts"
    )]
    [SwaggerResponse(204, "The expired scheduled discounts were cleaned up.")]
    public async Task<IActionResult> CleanupExpiredDiscounts()
    {
        var command = new CleanupExpiredProductDiscountsCommand();

        await scheduledDiscountCommandService.Handle(command);

        return NoContent();
    }

    [HttpPut("{discountId:int}")]
    [SwaggerOperation(
        Summary = "Update scheduled discount",
        Description = "Updates a scheduled discount",
        OperationId = "UpdateScheduledDiscount"
    )]
    [SwaggerResponse(204, "The scheduled discount was updated.")]
    [SwaggerResponse(404, "The scheduled discount was not found.")]
    public async Task<IActionResult> UpdateScheduledDiscount(int discountId, [FromBody] UpdateScheduledDiscountResource resource)
    {
        var command = UpdateScheduledDiscountCommandFromResourceAssembler.ToCommandFromResource(discountId, resource);

        await scheduledDiscountCommandService.Handle(command);

        return NoContent();
    }

    [HttpDelete("{discountId:int}")]
    [SwaggerOperation(
        Summary = "Delete scheduled discount",
        Description = "Deletes a scheduled discount",
        OperationId = "DeleteScheduledDiscount"
    )]
    [SwaggerResponse(204, "The scheduled discount was deleted.")]
    [SwaggerResponse(404, "The scheduled discount was not found.")]
    public async Task<IActionResult> DeleteScheduledDiscount(int discountId)
    {
        var command = new DeleteScheduledDiscountCommand(discountId);

        await scheduledDiscountCommandService.Handle(command);

        return NoContent();
    }

    [HttpPatch("{discountId:int}/disable")]
    [SwaggerOperation(
        Summary = "Disable scheduled discount",
        Description = "Disables a scheduled discount",
        OperationId = "DisableScheduledDiscount"
    )]
    [SwaggerResponse(204, "The scheduled discount was disabled.")]
    [SwaggerResponse(404, "The scheduled discount was not found.")]
    public async Task<IActionResult> DisableScheduledDiscount(int discountId)
    {
        var command = new DisableScheduledDiscountCommand(discountId);

        await scheduledDiscountCommandService.Handle(command);

        return NoContent();
    }
}