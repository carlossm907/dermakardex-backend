namespace Products.Domain.Model.Queries.SalesProductReport;

public record GetProductSalesReportQuery(
    int ProductId,
    DateOnly From,
    DateOnly To
);