using System.Net.Mime;
using dermakardex_backend.Products.Domain.Model.Commands.Product;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries;
using Products.Domain.Services;
using Products.Interfaces.REST.Resources;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Products Endpoints.")]
public class ProductsController(IProductCommandService productCommandService, IProductQueryService productQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Products", "Get all products.", OperationId = "GetProducts")]
    [SwaggerResponse(200, "The products were found and returned.", typeof(IEnumerable<ProductResource>))]
    [SwaggerResponse(404, "The products were not found.")]
    public async Task<IActionResult> GetProducts([FromQuery] string? name)
    {
        var query = new ListProductsQuery(name);
        var products = await productQueryService.Handle(query);

        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("{productId:int}")]
    [SwaggerOperation("Get Product by Id", "Get a product by its unique identifier.", OperationId = "GetProductById")]
    [SwaggerResponse(200, "The product was found and returned.", typeof(ProductResource))]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        var getProductByIdQuery = new GetProductByIdQuery(productId);
        var product = await productQueryService.Handle(getProductByIdQuery);

        if (product is null) return NotFound();

        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);

        return Ok(productResource);
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStockProducts()
    {
        var products = await productQueryService.Handle(
            new GetLowStockProductsQuery()
        );

        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation("Create product", "Create a new product.", OperationId = "CreateProduct")]
    [SwaggerResponse(201, "The product was created.", typeof(ProductResource))]
    [SwaggerResponse(400, "The product was not created.")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource resource)
    {
        var createProductCommand = CreateProductCommandFromResourceAssembler.ToCommandFromResource(resource);
        var product = await productCommandService.Handle(createProductCommand);

        if (product is null) return BadRequest();

        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);

        return CreatedAtAction(nameof(GetProductById), new { productId = product.Id }, productResource);

    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductResource resource)
    {
        var command =
            UpdateProductCommandFromResourceAssembler.ToCommandFromResource(
                productId,
                resource
            );

        var product = await productCommandService.Handle(command);

        if (product is null) return NotFound();

        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);

        return Ok(productResource);
    }

    // Discount Endpoints

    [HttpPost("{productId:int}/discount")]
    [SwaggerOperation(
    "Apply discount to product", "Apply a discount (amount or percentage) to a product.", OperationId = "ApplyDiscountToProduct")]
    [SwaggerResponse(204, "The discount was applied successfully.")]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> ApplyDiscountToProduct(int productId, [FromBody] ApplyDiscountResource resource)
    {
        var command = new ApplyDiscountToProductCommand(
            productId,
            resource.Type,
            resource.Value
        );

        await productCommandService.Handle(command);

        return NoContent();
    }

    [HttpDelete("{productId:int}/discount")]
    [SwaggerOperation("Remove product discount", "Remove the current discount from a product.", OperationId = "RemoveProductDiscount")]
    [SwaggerResponse(204, "The discount was removed successfully.")]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> RemoveDiscountFromProduct(int productId)
    {
        var command = new RemoveDiscountFromProductCommand(productId);

        await productCommandService.Handle(command);

        return NoContent();
    }

    [HttpDelete("discounts/all")]
    [SwaggerOperation(
    "Remove discount from all products",
    "Remove the current discount from all products.",
    OperationId = "RemoveDiscountFromAllProducts")]
    [SwaggerResponse(204, "The discount was removed from all products.")]
    public async Task<IActionResult> RemoveDiscountFromAllProducts()
    {
        var command = new RemoveDiscountFromAllProductsCommand();

        await productCommandService.Handle(command);

        return NoContent();
    }

    [HttpPost("discounts")]
    [SwaggerOperation("Apply discount to multiple products", "Apply a discount to a list of products.", OperationId = "ApplyDiscountToProducts")]
    [SwaggerResponse(204, "The discount was applied successfully.")]
    public async Task<IActionResult> ApplyDiscountToProducts([FromBody] ApplyDiscountToProductsCommand command)
    {
        await productCommandService.Handle(command);

        return NoContent();
    }

    [HttpPost("discounts/all")]
    [SwaggerOperation("Apply discount to all products", "Apply a discount to all products in the system.", OperationId = "ApplyDiscountToAllProducts")]
    [SwaggerResponse(204, "The discount was applied successfully.")]
    public async Task<IActionResult> ApplyDiscountToAllProducts([FromBody] ApplyDiscountResource resource)
    {
        var command = new ApplyDiscountToAllProductsCommand(
            resource.Type,
            resource.Value
        );

        await productCommandService.Handle(command);

        return NoContent();
    }



}