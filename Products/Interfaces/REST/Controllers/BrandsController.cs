using System.Net.Mime;
using dermakardex_backend.Products.Domain.Model.Commands.Brand;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Queries;
using Products.Domain.Services;
using Products.Interfaces.REST.Resources;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Brands Endpoints.")]
public class BrandsController(IBrandCommandService brandCommandService, IBrandQueryService brandQueryService) : ControllerBase
{

    [HttpGet]
    [SwaggerOperation("Get All Brands", "Get all brands.", OperationId = "GetAllBrands")]
    [SwaggerResponse(200, "The brands were found and returned.", typeof(IEnumerable<BrandResource>))]
    [SwaggerResponse(404, "The brands were not found.")]
    public async Task<IActionResult> GetAllBrands()
    {
        var getAllBrandsQuery = new GetAllBrandsQuery();
        var brands = await brandQueryService.Handle(getAllBrandsQuery);
        var brandResources = brands.Select(BrandResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(brandResources);
    }

    [HttpGet("{brandId:int}")]
    [SwaggerOperation("Get Brand by Id", "Get a brand by its unique identifier.", OperationId = "GetBrandById")]
    [SwaggerResponse(200, "The brand was found and returned.", typeof(BrandResource))]
    [SwaggerResponse(404, "The brand was not found.")]
    public async Task<IActionResult> GetBrandById(int brandId)
    {
        var getBrandByIdQuery = new GetBrandByIdQuery(brandId);
        var brand = await brandQueryService.Handle(getBrandByIdQuery);

        if (brand is null) return NotFound();

        var brandResource = BrandResourceFromEntityAssembler.ToResourceFromEntity(brand);

        return Ok(brandResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Brand", "Create a new brand.", OperationId = "CreateBrand")]
    [SwaggerResponse(201, "The brand was created.", typeof(BrandResource))]
    [SwaggerResponse(400, "The brand was not created.")]
    public async Task<IActionResult> CreateBrand(CreateBrandResource resource)
    {
        var createBrandCommand = CreateBrandCommandFromResourceAssembler.ToCommandFromResource(resource);
        var brand = await brandCommandService.Handle(createBrandCommand);
        if (brand is null) return BadRequest();

        var brandResource = BrandResourceFromEntityAssembler.ToResourceFromEntity(brand);

        return CreatedAtAction(nameof(GetBrandById), new { brandId = brand.Id }, brandResource);
    }

    [HttpPut("{brandId:int}")]
    [SwaggerOperation("Update Brand", "Update an existing brand.", OperationId = "UpdateBrand")]
    [SwaggerResponse(200, "The brand was updated.", typeof(BrandResource))]
    [SwaggerResponse(404, "The brand was not found.")]
    public async Task<IActionResult> UpdateBrand(int brandId, [FromBody] UpdateBrandResource resource)
    {
        var command = UpdateBrandCommandFromResourceAssembler.ToCommandFromResource(brandId, resource);
        var brand = await brandCommandService.Handle(command);

        if (brand is null) return NotFound();

        var brandResource = BrandResourceFromEntityAssembler.ToResourceFromEntity(brand);

        return Ok(brandResource);
    }

    [HttpDelete("{brandId:int}")]
    [SwaggerOperation("Delete Brand", "Delete an existing brand.", OperationId = "DeleteBrand")]
    [SwaggerResponse(204, "The brand was deleted.")]
    [SwaggerResponse(404, "The brand was not found.")]
    public async Task<IActionResult> DeleteBrand(int brandId)
    {
        var result = await brandCommandService.Handle(new DeleteBrandCommand(brandId));

        return result ? NoContent() : NotFound();
    }
}