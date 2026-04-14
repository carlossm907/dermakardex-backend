namespace Products.Domain.Model.Queries.StockReport;

public record GetAllProductsDailyStockReportQuery(
    DateOnly From,
    DateOnly To
);