namespace Sales.Domain.Model.Queries;

public record GetProductsSalesBetweenDatesQuery(
    DateOnly From,
    DateOnly To
);