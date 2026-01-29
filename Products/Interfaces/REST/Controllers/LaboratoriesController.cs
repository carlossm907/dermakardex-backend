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
public class Laboratories(ILaboratoryCommandService laboratoryCommandService, ILaboratoryQueryService laboratoryQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLaboratories()
    {
        var getAllLaboratoriesQuery = new GetAllLaboratoriesQuery();
        var labs = await laboratoryQueryService.Handle(getAllLaboratoriesQuery);
        var labsResources = labs.Select(LaboratoryResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(labsResources);

    }

    [HttpGet("{laboratoryId:int}")]
    public async Task<IActionResult> GetLaboratoryById(int laboratoryId)
    {
        var getLaboratoryIdQuery = new GetLaboratoryByIdQuery(laboratoryId);
        var laboratory = await laboratoryQueryService.Handle(getLaboratoryIdQuery);

        if (laboratory is null) return BadRequest();

        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);

        return Ok(laboratoryResource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLaboratory(CreateLaboratoryResource resource)
    {
        var createLaboratoryCommand = CreateLaboratoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var laboratory = await laboratoryCommandService.Handle(createLaboratoryCommand);

        if (laboratory is null) return BadRequest();

        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);

        return CreatedAtAction(nameof(GetLaboratoryById), new { laboratoryId = laboratory.Id }, laboratoryResource);
    }

    [HttpPut("{laboratoryId:int}")]
    public async Task<IActionResult> UpdateLaboratory(int laboratoryId, [FromBody] UpdateLaboratoryResource resource)
    {
        var command = UpdateLaboratoryCommandFromResourceAssembler.ToCommandFromResource(laboratoryId, resource);
        var laboratory = await laboratoryCommandService.Handle(command);

        if (laboratory is null) return NotFound();

        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);

        return Ok(laboratoryResource);

    }

    [HttpDelete("{laboratoryId:int}")]
    public async Task<IActionResult> DeleteLaboratory(int laboratoryId)
    {
        var result = await laboratoryCommandService.Handle(new DeleteLaboratoryCommand(laboratoryId));

        return result ? NoContent() : NotFound();
    }

}