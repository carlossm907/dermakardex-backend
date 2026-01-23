using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class CreateCategoryCommandFromResourceAssembler
{
    public static CreateCategoryCommand ToCommandFromResource(CreateCategoryResource resource)
    {
        return new CreateCategoryCommand(
            resource.Name
        );
    }
}
