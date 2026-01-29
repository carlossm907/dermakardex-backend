using System.Net.Mime;
using dermakardex_backend.Products.Domain.Model.Commands.Supplier;
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
[SwaggerTag("Suppliers Endpoints.")]
public class SuppliersController(ISupplierCommandService supplierCommandService, ISupplierQueryService supplierQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Suppliers", "Get all suppliers.", OperationId = "GetAllSuppliers")]
    [SwaggerResponse(200, "The suppliers were found and returned.", typeof(IEnumerable<SupplierResource>))]
    [SwaggerResponse(404, "The suppliers were not found.")]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var getAllSuppliersQuery = new GetAllSuppliersQuery();
        var suppliers = await supplierQueryService.Handle(getAllSuppliersQuery);
        var supplierResources = suppliers.Select(SupplierResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(supplierResources);
    }

    [HttpGet("{supplierId:int}")]
    [SwaggerOperation("Get Supplier by Id", "Get a supplier by its unique identifier.", OperationId = "GetSupplierById")]
    [SwaggerResponse(200, "The supplier was found and returned.", typeof(SupplierResource))]
    [SwaggerResponse(404, "The supplier was not found.")]
    public async Task<IActionResult> GetSupplierById(int supplierId)
    {
        var getSupplierByIdQuery = new GetSupplierByIdQuery(supplierId);
        var supplier = await supplierQueryService.Handle(getSupplierByIdQuery);

        if (supplier is null) return NotFound();

        var supplierResources = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);

        return Ok(supplierResources);
    }

    [HttpPost]
    [SwaggerOperation("Create Supplier", "Create a new supplier.", OperationId = "CreateSupplier")]
    [SwaggerResponse(201, "The supplier was created.", typeof(SupplierResource))]
    [SwaggerResponse(400, "The supplier was not created.")]
    public async Task<IActionResult> CreateSupplier(CreateSupplierResource resource)
    {
        var createSupplierCommand = CreateSupplierCommandFromResourceAssembler.ToCommandFromResource(resource);
        var supplier = await supplierCommandService.Handle(createSupplierCommand);

        if (supplier is null) return BadRequest();

        var supplierResource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);

        return CreatedAtAction(nameof(GetSupplierById), new { supplierId = supplier.Id }, supplierResource);
    }

    [HttpPut("{supplierId:int}")]
    [SwaggerOperation("Update Supplier", "Update an existing supplier.", OperationId = "UpdateSupplier")]
    [SwaggerResponse(200, "The supplier was updated.", typeof(SupplierResource))]
    [SwaggerResponse(404, "The supplier was not found.")]
    public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] UpdateSupplierResource resource)
    {
        var command = UpdateSupplierCommandFromResourceAssembler.ToCommandFromResource(supplierId, resource);
        var supplier = await supplierCommandService.Handle(command);

        if (supplier is null) return NotFound();

        var supplierResource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);

        return Ok(supplierResource);
    }

    [HttpDelete("{supplierId:int}")]
    [SwaggerOperation("Delete Supplier", "Delete an existing supplier.", OperationId = "DeleteSupplier")]
    [SwaggerResponse(200, "The supplier was deleted.")]
    [SwaggerResponse(404, "The supplier was not found.")]
    public async Task<IActionResult> DeleteSupplier(int supplierId)
    {
        var result = await supplierCommandService.Handle(new DeleteSupplierCommand(supplierId));

        return result ? NoContent() : NotFound();
    }

}