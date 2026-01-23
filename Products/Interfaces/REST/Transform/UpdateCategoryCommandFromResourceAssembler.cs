using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class UpdateCategoryCommandFromResourceAssembler
{
    public static UpdateCategoryCommand ToCommandFromResource(int id, UpdateCategoryResource resource)
    {
        return new UpdateCategoryCommand(
            id,
            resource.Name
        );
    }
}
