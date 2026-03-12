namespace Sales.Interfaces.REST.Resources;

public record SaleGroupedReportProductResource(
    int ProductId,
    string ProductName,
    string Presentation,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal
);