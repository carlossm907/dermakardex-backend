using Products.Domain.Model.Entities;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier supplier)
    {
        return new SupplierResource(
            supplier.Id,
            supplier.Name
        );
    }
}
