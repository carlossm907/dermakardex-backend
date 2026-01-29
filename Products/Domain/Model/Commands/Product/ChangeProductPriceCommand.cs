namespace Products.Domain.Model.Commands;

public record ChangeProductPricesCommand(
    int ProductId,
    decimal PurchasePrice,
    decimal SalePrice
);