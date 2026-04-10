namespace Products.Domain.Model.Queries;

public record GetAffectedProductsDailyStockReportQuery(
    DateOnly From,
    DateOnly To
);