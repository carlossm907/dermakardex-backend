namespace Products.Interfaces.REST.Resources.Reports;

public record ProductEntriesReportResource(
    int ProductId,
    string ProductName,
    DateOnly From,
    DateOnly To,
    int Quantity
);