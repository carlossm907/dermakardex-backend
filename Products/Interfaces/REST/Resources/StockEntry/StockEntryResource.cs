namespace Products.Interfaces.REST.Resources;

public record StockEntryResource(
    int Id,
    int ProductId,
    string ProductName,
    int Quantity,
    DateOnly ExpirationDate,
    decimal UnitPurchasePrice,
    decimal TotalInvestment,
    string Reason,
    string UserFullName,
    DateTime RegisteredAt
);