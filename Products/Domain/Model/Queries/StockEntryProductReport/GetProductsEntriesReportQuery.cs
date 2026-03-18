namespace Products.Domain.Model.Queries.StockEntryProductReport;

public record GetProductsEntriesReportQuery(
    IEnumerable<int> ProductIds,
    DateOnly From,
    DateOnly To
);