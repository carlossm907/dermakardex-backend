namespace Products.Interfaces.REST.Resources;

public record ProductSalesReportResource(
    int ProductId,
    string ProductName,
    DateOnly From,
    DateOnly To,
    int Quantity
);