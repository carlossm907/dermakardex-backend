using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class CreateLaboratoryCommandFromResourceAssembler
{
    public static CreateLaboratoryCommand ToCommandFromResource(CreateLaboratoryResource resource)
    {
        return new CreateLaboratoryCommand(
            resource.Name
        );
    }
}
