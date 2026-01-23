using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Model.Commands;
using Products.Domain.Model.Queries;
using Products.Domain.Services;
using Products.Interfaces.REST.Resources;
using Products.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Categories Endpoints.")]
public class CategoriesController(ICategoryCommandService categoryCommandService, ICategoryQueryService categoryQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var getAllCategoriesQuery = new GetAllCategoriesQuery();
        var categories = await categoryQueryService.Handle(getAllCategoriesQuery);
        var categoriesResource = categories.Select(CategoryResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(categories);
    }

    [HttpGet("{categoryId:int}")]
    public async Task<IActionResult> GetCategoryId(int categoryId)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery);

        if (category is null) return BadRequest();

        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);

        return Ok(categoryResource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryResource resource)
    {
        var createCategoryCommand = CreateCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var category = await categoryCommandService.Handle(createCategoryCommand);
        if (category is null) return BadRequest();

        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);

        return CreatedAtAction(nameof(GetCategoryId), new { categoryId = category.Id }, categoryResource);
    }

    [HttpPut("{categoryId:int}")]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryResource resource)
    {
        var command = UpdateCategoryCommandFromResourceAssembler.ToCommandFromResource(categoryId, resource);
        var category = await categoryCommandService.Handle(command);
        if (category is null) return BadRequest();

        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);

        return Ok(categoryResource);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        var result = await categoryCommandService.Handle(new DeleteCategoryCommand(categoryId));

        return result ? NoContent() : NotFound();
    }


}