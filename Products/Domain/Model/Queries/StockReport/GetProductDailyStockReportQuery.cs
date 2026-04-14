namespace Products.Domain.Model.Queries.StockReport;

public record GetProductDailyStockReportQuery(
    int ProductId,
    DateOnly From,
    DateOnly To
);