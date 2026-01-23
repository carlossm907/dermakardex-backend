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
[SwaggerTag("Suppliers Endpoints.")]
public class SuppliersController(ISupplierCommandService supplierCommandService, ISupplierQueryService supplierQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var getAllSuppliersQuery = new GetAllSuppliersQuery();
        var suppliers = await supplierQueryService.Handle(getAllSuppliersQuery);
        var supplierResources = suppliers.Select(SupplierResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(supplierResources);
    }

    [HttpGet("{supplierId:int}")]
    public async Task<IActionResult> GetSupplierById(int supplierId)
    {
        var getSupplierByIdQuery = new GetSupplierByIdQuery(supplierId);
        var supplier = await supplierQueryService.Handle(getSupplierByIdQuery);

        if (supplier is null) return NotFound();

        var supplierResources = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);

        return Ok(supplierResources);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSupplier(CreateSupplierResource resource)
    {
        var createSupplierCommand = CreateSupplierCommandFromResourceAssembler.ToCommandFromResource(resource);
        var supplier = await supplierCommandService.Handle(createSupplierCommand);

        if (supplier is null) return BadRequest();

        var supplierResource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);

        return CreatedAtAction(nameof(GetSupplierById), new { supplierId = supplier.Id }, supplierResource);
    }

    [HttpPut("{supplierId:int}")]
    public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] UpdateSupplierResource resource)
    {
        var command = UpdateSupplierCommandFromResourceAssembler.ToCommandFromResource(supplierId, resource);
        var supplier = await supplierCommandService.Handle(command);

        if (supplier is null) return NotFound();

        var supplierResource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);

        return Ok(supplierResource);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSupplier(int supplierId)
    {
        var result = await supplierCommandService.Handle(new DeleteSupplierCommand(supplierId));

        return result ? NoContent() : NotFound();
    }

}