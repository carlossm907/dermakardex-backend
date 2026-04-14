namespace Products.Domain.Model.Queries.StockReport;

public record GetAffectedProductsDailyStockReportQuery(
    DateOnly From,
    DateOnly To
);