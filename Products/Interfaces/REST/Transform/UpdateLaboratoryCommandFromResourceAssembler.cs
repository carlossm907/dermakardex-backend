using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class UpdateLaboratoryCommandFromResourceAssembler
{
    public static UpdateLaboratoryCommand ToCommandFromResource(int id, UpdateLaboratoryResource resource)
    {
        return new UpdateLaboratoryCommand(
            id,
            resource.Name
        );
    }
}
