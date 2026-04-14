using Products.Domain.Model.Entities;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class StockEntryResourceFromEntityAssembler
{
    public static StockEntryResource ToResourceFromEntity(
        StockEntry entry
    )
    {
        return new StockEntryResource(
            entry.Id,
            entry.ProductId,
            entry.ProductName,
            entry.Quantity,
            entry.ExpirationDate,
            entry.UnitPurchasePrice.Amount,
            entry.TotalInvestment.Amount,
            entry.Reason,
            entry.UserFullName,
            entry.RegisteredAt
        );
    }
}
