namespace Products.Interfaces.REST.Resources;

public record CreateStockEntryResource(
    int Quantity,
    DateOnly ExpirationDate,
    decimal UnitPurchasePrice,
    string Reason
);