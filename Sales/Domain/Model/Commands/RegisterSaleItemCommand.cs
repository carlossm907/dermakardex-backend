namespace Sales.Domain.Model.Commands;

public record RegisterSaleItemCommand(
    int ProductId,
    int Quantity
);