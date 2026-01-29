namespace dermakardex_backend.Products.Domain.Model.Commands.Product;

public record ChangeProductMaxDiscountCommand(
    int ProductId,
    decimal MaxDiscountAmount
);