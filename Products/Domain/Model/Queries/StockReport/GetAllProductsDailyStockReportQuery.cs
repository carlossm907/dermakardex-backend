namespace Products.Domain.Model.Queries;

public record GetAllProductsDailyStockReportQuery(
    DateOnly From,
    DateOnly To
);