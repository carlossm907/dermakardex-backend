namespace Products.Domain.Model.Queries.StockEntryProductReport;

public record GetAffectedProductsEntriesReportQuery(
    DateOnly From,
    DateOnly To
);