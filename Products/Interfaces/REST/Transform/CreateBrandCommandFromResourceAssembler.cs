using dermakardex_backend.Products.Domain.Model.Commands.Brand;
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
