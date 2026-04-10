namespace Products.Domain.Model.Queries;

public record GetAffectedProductsEntriesReportQuery(
    DateOnly From,
    DateOnly To
);