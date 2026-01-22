namespace Products.Domain.Model.Commands;

public record AdjustProductStockCommand(
    int ProductId,
    int Delta
);