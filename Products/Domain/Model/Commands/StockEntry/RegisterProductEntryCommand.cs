namespace Products.Domain.Model.Commands;

public record RegisterProductEntryCommand(
    int ProductId,
    int Quantity,
    decimal UnitPurchasePrice,
    string Reason,
    int RegisteredByUserId
);