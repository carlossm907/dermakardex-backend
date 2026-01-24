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
    [SwaggerOperation("Get All Categories", "Get all categories.", OperationId = "GetAllCategories")]
    [SwaggerResponse(200, "The categories were found and returned.", typeof(IEnumerable<CategoryResource>))]
    [SwaggerResponse(404, "The categories were not found.")]
    public async Task<IActionResult> GetAllCategories()
    {
        var getAllCategoriesQuery = new GetAllCategoriesQuery();
        var categories = await categoryQueryService.Handle(getAllCategoriesQuery);
        var categoriesResource = categories.Select(CategoryResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(categories);
    }

    [HttpGet("{categoryId:int}")]
    [SwaggerOperation("Get Category by Id", "Get a category by its unique identifier.", OperationId = "GetCategoryById")]
    [SwaggerResponse(200, "The category was found and returned.", typeof(CategoryResource))]
    [SwaggerResponse(404, "The category was not found.")]
    public async Task<IActionResult> GetCategoryId(int categoryId)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery);

        if (category is null) return BadRequest();

        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);

        return Ok(categoryResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Category", "Create a new category.", OperationId = "CreateCategory")]
    [SwaggerResponse(201, "The category was created.", typeof(CategoryResource))]
    [SwaggerResponse(400, "The category was not created.")]
    public async Task<IActionResult> CreateCategory(CreateCategoryResource resource)
    {
        var createCategoryCommand = CreateCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var category = await categoryCommandService.Handle(createCategoryCommand);
        if (category is null) return BadRequest();

        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);

        return CreatedAtAction(nameof(GetCategoryId), new { categoryId = category.Id }, categoryResource);
    }

    [HttpPut("{categoryId:int}")]
    [SwaggerOperation("Update Category", "Update an existing category.", OperationId = "UpdateCategory")]
    [SwaggerResponse(200, "The category was updated.", typeof(CategoryResource))]
    [SwaggerResponse(404, "The category was not found.")]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryResource resource)
    {
        var command = UpdateCategoryCommandFromResourceAssembler.ToCommandFromResource(categoryId, resource);
        var category = await categoryCommandService.Handle(command);
        if (category is null) return BadRequest();

        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);

        return Ok(categoryResource);
    }

    [HttpDelete("{categoryId:int}")]
    [SwaggerOperation("Delete Category", "Delete an existing category.", OperationId = "DeleteCategory")]
    [SwaggerResponse(200, "The category was deleted.")]
    [SwaggerResponse(404, "The category was not found.")]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        var result = await categoryCommandService.Handle(new DeleteCategoryCommand(categoryId));

        return result ? NoContent() : NotFound();
    }


}