namespace Products.Domain.Model.Queries;

public record GetProductDailyStockReportQuery(
    int ProductId,
    DateOnly From,
    DateOnly To
);