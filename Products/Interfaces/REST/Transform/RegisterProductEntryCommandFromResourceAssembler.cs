using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class RegisterProductEntryCommandFromResourceAssembler
{
    public static RegisterProductEntryCommand ToCommandFromResource(
        int productId,
        int registeredByUserId,
        CreateStockEntryResource resource
    )
    {
        return new RegisterProductEntryCommand(
            productId,
            resource.Quantity,
            resource.UnitPurchasePrice,
            resource.Reason,
            registeredByUserId
        );
    }
}
