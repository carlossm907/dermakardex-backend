namespace Products.Domain.Model.Queries.StockReport;

public record GetProductsDailyStockReportQuery(
    IEnumerable<int> ProductIds,
    DateOnly From,
    DateOnly To
);