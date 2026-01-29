namespace dermakardex_backend.Products.Domain.Model.Commands.Product;

public record AdjustProductStockCommand(
    int ProductId,
    int Delta
);