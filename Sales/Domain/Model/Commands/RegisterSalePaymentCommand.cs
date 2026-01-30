using Sales.Domain.Model.ValueObjects;

namespace Sales.Domain.Model.Commands;

public record RegisterSalePaymentCommand(
    PaymentMethod Method,
    decimal Amount
);