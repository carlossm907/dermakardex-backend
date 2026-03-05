using dermakardex_backend.Products.Domain.Model.Commands.StockEntry;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class RegisterProductEntryCommandFromResourceAssembler
{
    public static RegisterProductEntryCommand ToCommandFromResource(
        int productId,
        CreateStockEntryResource resource
    )
    {
        return new RegisterProductEntryCommand(
            productId,
            resource.Quantity,
            resource.ExpirationDate,
            resource.UnitPurchasePrice,
            resource.Reason
        );
    }
}
