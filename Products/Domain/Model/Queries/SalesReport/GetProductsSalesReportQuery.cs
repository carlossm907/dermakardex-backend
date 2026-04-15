namespace Products.Domain.Model.Queries.SalesProductReport;

public record GetProductsSalesReportQuery(
    IEnumerable<int> ProductIds,
    DateOnly From,
    DateOnly To
);