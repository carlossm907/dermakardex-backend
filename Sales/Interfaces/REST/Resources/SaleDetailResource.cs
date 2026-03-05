namespace Sales.Interfaces.REST.Resources;

public record SaleDetailResource(
    int Id,
    string TicketNumber,
    string CustomerDni,
    string CustomerFullName,
    int SellerUserId,
    string SellerFullName,
    DateOnly SaleDate,
    TimeOnly SaleTime,
    string? Observation,
    decimal Total,
    string Status,
    IReadOnlyCollection<SaleItemResource> Items,
    IReadOnlyCollection<SalePaymentResource> Payments
);
