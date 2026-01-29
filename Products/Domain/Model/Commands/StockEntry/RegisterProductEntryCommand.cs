namespace dermakardex_backend.Products.Domain.Model.Commands.StockEntry;

public record RegisterProductEntryCommand(
    int ProductId,
    int Quantity,
    decimal UnitPurchasePrice,
    string Reason,
    int RegisteredByUserId
);