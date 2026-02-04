namespace Sales.Interfaces.REST.Resources;

public record RegisterSaleResource(
    string CustomerDni,
    string? Observation,
    IReadOnlyCollection<RegisterSaleItemResource> Items,
    IReadOnlyCollection<RegisterSalePaymentResource> Payments
);

public record RegisterSaleItemResource(
    int ProductId,
    int Quantity
);

public record RegisterSalePaymentResource(
    string Method,
    decimal Amount
);