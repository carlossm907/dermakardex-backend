namespace Products.Interfaces.REST.Resources.Reports;

public record ProductSalesReportResource(
    int ProductId,
    string ProductName,
    DateOnly From,
    DateOnly To,
    int Quantity
);