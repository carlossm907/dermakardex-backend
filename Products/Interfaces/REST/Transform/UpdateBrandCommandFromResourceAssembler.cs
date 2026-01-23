using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class UpdateBrandCommandFromResourceAssembler
{
    public static UpdateBrandCommand ToCommandFromResource(int id, UpdateBrandResource resource)
    {
        return new UpdateBrandCommand(
            id,
            resource.Name
        );
    }
}
