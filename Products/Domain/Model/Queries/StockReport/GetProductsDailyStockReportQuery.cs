namespace Products.Domain.Model.Queries;

public record GetProductsDailyStockReportQuery(
    IEnumerable<int> ProductIds,
    DateOnly From,
    DateOnly To
);