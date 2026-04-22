using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Internal.OutboundServices;
using Sales.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Sales.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/customers")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Customers Endpoints.")]
public class DniLookupController(

    IDniLookupService dniLookupService

) : ControllerBase

{
    [HttpGet("dni/{dni}")]
    [SwaggerOperation(
    Summary = "Get customer full name by DNI",
    Description = "Looks up the full name associated with the given 8-digit DNI using ApiPeru.",
    OperationId = "GetCustomerFullNameByDni")]
    [SwaggerResponse(200, "DNI found.")]
    [SwaggerResponse(400, "Invalid DNI format.")]
    [SwaggerResponse(404, "DNI not found in the registry.")]
    public async Task<IActionResult> GetCustomerFullNameByDni(string dni)
    {
        if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8 || !dni.All(char.IsDigit))
            return BadRequest(new { error = "El DNI debe tener exactamente 8 dígitos numéricos." });

        var fullName = await dniLookupService.GetFullNameByDniAsync(dni);

        if (fullName is null)
            return NotFound(new { error = "No se encontró información para el DNI ingresado." });

        var resource = DniLookupResourceFromStringAssembler.ToResourceFromFullName(fullName);
        return Ok(resource);
    }
}