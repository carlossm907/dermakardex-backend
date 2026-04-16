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
public class ProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Products", "Get all products.", OperationId = "GetProducts")]
    [SwaggerResponse(200, "The products were found and returned.", typeof(IEnumerable<ProductResource>))]
    public async Task<IActionResult> GetAllProducts([FromQuery] string? name)
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
    [SwaggerOperation("Get Low Stock Products", "Get all products with stock below alert threshold.", OperationId = "GetLowStockProducts")]
    [SwaggerResponse(200, "The low stock products were found and returned.", typeof(IEnumerable<ProductResource>))]
    public async Task<IActionResult> GetLowStockProducts()
    {
        var products = await productQueryService.Handle(
            new GetLowStockProductsQuery()
        );

        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("by-code/{code}")]
    [SwaggerOperation("Get Product by Code", "Get a product by its code", OperationId = "GetProductByCode")]
    [SwaggerResponse(200, "The product was found and returned.", typeof(ProductResource))]
    [SwaggerResponse(404, "The product was not found.")]
    public async Task<IActionResult> GetProductByCode(string code)
    {
        var getProductByCodeQuery = new GetProductByCodeQuery(code);
        var product = await productQueryService.Handle(getProductByCodeQuery);

        if (product is null) return NotFound();

        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);

        return Ok(productResource);
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
    [SwaggerOperation("Update Product", "Update an existing product.", OperationId = "UpdateProduct")]
    [SwaggerResponse(200, "The product was updated.", typeof(ProductResource))]
    [SwaggerResponse(404, "The product was not found.")]
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

}