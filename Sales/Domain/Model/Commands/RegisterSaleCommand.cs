namespace Sales.Domain.Model.Commands;

public record RegisterSaleCommand(
    string CustomerDni,
    string Observation,
    IReadOnlyCollection<RegisterSaleItemCommand> Items,
    IReadOnlyCollection<RegisterSalePaymentCommand> Payments
);