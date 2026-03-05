namespace Sales.Interfaces.REST.Resources;

public record SaleResource(
    int Id,
    string TicketNumber,
    string CustomerFullName,
    string SellerFullName,
    DateOnly SaleDate,
    TimeOnly SaleTime,
    decimal Total,
    string Status
);