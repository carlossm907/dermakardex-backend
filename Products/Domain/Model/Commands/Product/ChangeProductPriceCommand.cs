namespace dermakardex_backend.Products.Domain.Model.Commands.Product;

public record ChangeProductPricesCommand(
    int ProductId,
    decimal PurchasePrice,
    decimal SalePrice
);