using Products.Domain.Model.Entities;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class LaboratoryResourceFromEntityAssembler
{
    public static LaboratoryResource ToResourceFromEntity(Laboratory laboratory)
    {
        return new LaboratoryResource(
            laboratory.Id,
            laboratory.Name
        );
    }
}
