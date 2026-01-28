namespace Products.Interfaces.REST.Resources;

public record StockEntryResource(
    int Id,
    int ProductId,
    int Quantity,
    decimal UnitPurchasePrice,
    decimal TotalInvestment,
    string Reason,
    int RegisteredByUserId,
    string RegisteredByUserName,
    DateTime RegisteredAt
);