namespace Sales.Domain.Model.Queries;

public record GetProductSalesByProductBetweenDatesQuery(
    int ProductId,
    DateOnly From,
    DateOnly To
);