namespace Products.Domain.Model.Queries.StockEntryProductReport;

public record GetProductEntriesReportQuery(
    int ProductId,
    DateOnly From,
    DateOnly To
);