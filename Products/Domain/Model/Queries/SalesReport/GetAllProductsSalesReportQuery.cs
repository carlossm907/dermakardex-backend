namespace Products.Domain.Model.Queries.SalesProductReport;

public record GetAllProductsSalesReportQuery(
    DateOnly From,
    DateOnly To
);