namespace Products.Domain.Model.Queries.SalesProductReport;

public record GetAffectedProductsSalesReportQuery(
    DateOnly From,
    DateOnly To
);