namespace Products.Interfaces.REST.Resources.Reports;

public record ProductDailyStockReportResource(
    int ProductId,
    string ProductName,
    DateOnly Date,
    int InitialStock,
    int Entries,
    int Sold,
    int FinalStock
);