using dermakardex_backend.Products.Domain.Model.Commands.Laboratory;
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
