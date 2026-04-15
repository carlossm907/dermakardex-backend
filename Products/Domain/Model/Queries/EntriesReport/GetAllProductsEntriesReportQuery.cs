namespace Products.Domain.Model.Queries.StockEntryProductReport;


public record GetAllProductsEntriesReportQuery(
    DateOnly From,
    DateOnly To
);