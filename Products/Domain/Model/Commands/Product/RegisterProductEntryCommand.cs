namespace dermakardex_backend.Products.Domain.Model.Commands.StockEntry;

public record RegisterProductEntryCommand(
    int ProductId,
    int Quantity,
    DateOnly ExpirationDate,
    decimal UnitPurchasePrice,
    string Reason
);