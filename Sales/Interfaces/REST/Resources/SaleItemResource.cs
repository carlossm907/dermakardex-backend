namespace Sales.Interfaces.REST.Resources;

public record SaleItemResource(
    int ProductId,
    string ProductName,
    string Presentation,
    int Quantity,
    decimal BaseUnitPrice,
    decimal UnitPrice,
    string DiscountType,
    decimal DiscountValue,
    decimal LineTotal
);