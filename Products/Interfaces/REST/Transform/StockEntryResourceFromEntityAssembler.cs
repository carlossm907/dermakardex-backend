using Products.Domain.Model.Entities;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class StockEntryResourceFromEntityAssembler
{
    public static StockEntryResource ToResourceFromEntity(
        StockEntry entry,
        string registeredByUserName
    )
    {
        return new StockEntryResource(
            entry.Id,
            entry.ProductId,
            entry.Quantity,
            entry.UnitPurchasePrice.Amount,
            entry.TotalInvestment.Amount,
            entry.Reason,
            entry.RegisteredByUserId,
            registeredByUserName,
            entry.RegisteredAt
        );
    }
}
