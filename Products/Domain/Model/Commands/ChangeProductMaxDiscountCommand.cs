namespace Products.Domain.Model.Commands;

public record ChangeProductMaxDiscountCommand(
    int ProductId,
    decimal MaxDiscountAmount
);