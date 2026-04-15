using System.Net.Mime;
using dermakardex_backend.Products.Domain.Model.Commands.Laboratory;
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
[SwaggerTag("Laboratories Endpoints.")]
public class LaboratoriesController(ILaboratoryCommandService laboratoryCommandService, ILaboratoryQueryService laboratoryQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Laboratories", "Get all laboratories.", OperationId = "GetAllLaboratories")]
    [SwaggerResponse(200, "The laboratories were found and returned.", typeof(IEnumerable<LaboratoryResource>))]
    [SwaggerResponse(404, "The laboratories were not found.")]
    public async Task<IActionResult> GetAllLaboratories()
    {
        var getAllLaboratoriesQuery = new GetAllLaboratoriesQuery();
        var labs = await laboratoryQueryService.Handle(getAllLaboratoriesQuery);
        var labsResources = labs.Select(LaboratoryResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(labsResources);
    }

    [HttpGet("{laboratoryId:int}")]
    [SwaggerOperation("Get Laboratory by Id", "Get a laboratory by its unique identifier.", OperationId = "GetLaboratoryById")]
    [SwaggerResponse(200, "The laboratory was found and returned.", typeof(LaboratoryResource))]
    [SwaggerResponse(404, "The laboratory was not found.")]
    public async Task<IActionResult> GetLaboratoryById(int laboratoryId)
    {
        var getLaboratoryIdQuery = new GetLaboratoryByIdQuery(laboratoryId);
        var laboratory = await laboratoryQueryService.Handle(getLaboratoryIdQuery);

        if (laboratory is null) return NotFound();

        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);

        return Ok(laboratoryResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Laboratory", "Create a new laboratory.", OperationId = "CreateLaboratory")]
    [SwaggerResponse(201, "The laboratory was created.", typeof(LaboratoryResource))]
    [SwaggerResponse(400, "The laboratory was not created.")]
    public async Task<IActionResult> CreateLaboratory(CreateLaboratoryResource resource)
    {
        var createLaboratoryCommand = CreateLaboratoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var laboratory = await laboratoryCommandService.Handle(createLaboratoryCommand);

        if (laboratory is null) return BadRequest();

        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);

        return CreatedAtAction(nameof(GetLaboratoryById), new { laboratoryId = laboratory.Id }, laboratoryResource);
    }

    [HttpPut("{laboratoryId:int}")]
    [SwaggerOperation("Update Laboratory", "Update an existing laboratory.", OperationId = "UpdateLaboratory")]
    [SwaggerResponse(200, "The laboratory was updated.", typeof(LaboratoryResource))]
    [SwaggerResponse(404, "The laboratory was not found.")]
    public async Task<IActionResult> UpdateLaboratory(int laboratoryId, [FromBody] UpdateLaboratoryResource resource)
    {
        var command = UpdateLaboratoryCommandFromResourceAssembler.ToCommandFromResource(laboratoryId, resource);
        var laboratory = await laboratoryCommandService.Handle(command);

        if (laboratory is null) return NotFound();

        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);

        return Ok(laboratoryResource);
    }

    [HttpDelete("{laboratoryId:int}")]
    [SwaggerOperation("Delete Laboratory", "Delete an existing laboratory.", OperationId = "DeleteLaboratory")]
    [SwaggerResponse(200, "The laboratory was deleted.")]
    [SwaggerResponse(404, "The laboratory was not found.")]
    public async Task<IActionResult> DeleteLaboratory(int laboratoryId)
    {
        var result = await laboratoryCommandService.Handle(new DeleteLaboratoryCommand(laboratoryId));

        return result ? NoContent() : NotFound();
    }
}