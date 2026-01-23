using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class UpdateSupplierCommandFromResourceAssembler
{
    public static UpdateSupplierCommand ToCommandFromResource(int id, UpdateSupplierResource resource)
    {
        return new UpdateSupplierCommand(
            id,
            resource.Name
        );
    }
}
