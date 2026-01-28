namespace Products.Interfaces.REST.Resources;

public record CreateStockEntryResource(
    int Quantity,
    decimal UnitPurchasePrice,
    string Reason
);