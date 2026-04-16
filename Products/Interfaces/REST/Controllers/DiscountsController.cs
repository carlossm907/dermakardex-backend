using System.Net.Mime;
using dermakardex_backend.Products.Domain.Model.Commands.Discounts;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Services;
using Products.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Discounts Endpoints.")]
public class DiscountsController(IDiscountsCommandService discountsCommandService) : ControllerBase
{
    [HttpPost("{productId:int}/discount")]
    [SwaggerOperation(
        Summary = "Apply discount to product",
        Description = "Apply a discount (amount or percentage) to a product.",
        OperationId = "ApplyDiscountToProduct"
    )]
    [SwaggerResponse(204, "The discount was applied successfully.")]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> ApplyDiscountToProduct(int productId, [FromBody] ApplyDiscountResource resource)
    {
        var command = new ApplyDiscountToProductCommand(
            productId,
            resource.Type,
            resource.Value
        );

        await discountsCommandService.Handle(command);

        return NoContent();
    }

    [HttpDelete("{productId:int}/discount")]
    [SwaggerOperation(
        Summary = "Remove product discount",
        Description = "Remove the current discount from a product.",
        OperationId = "RemoveProductDiscount"
    )]
    [SwaggerResponse(204, "The discount was removed successfully.")]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> RemoveDiscountFromProduct(int productId)
    {
        var command = new RemoveDiscountFromProductCommand(productId);

        await discountsCommandService.Handle(command);

        return NoContent();
    }

    [HttpDelete("discounts/all")]
    [SwaggerOperation(
        Summary = "Remove discount from all products",
        Description = "Remove the current discount from all products.",
        OperationId = "RemoveDiscountFromAllProducts"
    )]
    [SwaggerResponse(204, "The discount was removed from all products.")]
    public async Task<IActionResult> RemoveDiscountFromAllProducts()
    {
        var command = new RemoveDiscountFromAllProductsCommand();

        await discountsCommandService.Handle(command);

        return NoContent();
    }

    [HttpPost("bulk")]
    [SwaggerOperation(
        Summary = "Apply discount to multiple products",
        Description = "Apply a discount to a list of products.",
        OperationId = "ApplyDiscountToProducts"
    )]
    [SwaggerResponse(204, "The discount was applied successfully.")]
    [SwaggerResponse(400, "Invalid product IDs provided.")]
    public async Task<IActionResult> ApplyDiscountToProducts([FromBody] ApplyDiscountToProductsCommand command)
    {
        await discountsCommandService.Handle(command);

        return NoContent();
    }

    [HttpPost("all")]
    [SwaggerOperation(
        Summary = "Apply discount to all products",
        Description = "Apply a discount to all products in the system.",
        OperationId = "ApplyDiscountToAllProducts"
    )]
    [SwaggerResponse(204, "The discount was applied successfully.")]
    public async Task<IActionResult> ApplyDiscountToAllProducts([FromBody] ApplyDiscountResource resource)
    {
        var command = new ApplyDiscountToAllProductsCommand(
            resource.Type,
            resource.Value
        );

        await discountsCommandService.Handle(command);

        return NoContent();
    }
}