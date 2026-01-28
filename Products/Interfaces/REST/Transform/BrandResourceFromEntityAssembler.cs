using Products.Domain.Model.Entities;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class BrandResourceFromEntityAssembler
{
    public static BrandResource ToResourceFromEntity(Brand brand)
    {
        return new BrandResource(
            brand.Id,
            brand.Name
        );
    }
}
