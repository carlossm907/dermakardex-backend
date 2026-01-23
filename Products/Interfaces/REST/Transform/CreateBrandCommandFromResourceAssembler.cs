using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class CreateBrandCommandFromResourceAssembler
{
    public static CreateBrandCommand ToCommandFromResource(CreateBrandResource resource)
    {
        return new CreateBrandCommand(
            resource.Name
        );
    }
}
