namespace Sales.Interfaces.REST.Resources;

public record SalePaymentResource(
    string Method,
    decimal Amount
);