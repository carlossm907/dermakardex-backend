using Sales.Domain.Model.Commands;
using Sales.Domain.Model.ValueObjects;
using Sales.Interfaces.REST.Resources;

namespace Sales.Interfaces.REST.Transform;

public static class RegisterSaleCommandFromResourceAssembler
{
    public static RegisterSaleCommand ToCommandFromResource(RegisterSaleResource resource)
    {
        return new RegisterSaleCommand(
            resource.CustomerDni,
            resource.Observation ?? string.Empty,
            resource.Items.Select(i => new RegisterSaleItemCommand(i.ProductId, i.Quantity)).ToList(),
            resource.Payments.Select(p => new RegisterSalePaymentCommand(Enum.Parse<PaymentMethod>(p.Method), p.Amount)).ToList()
        );
    }
}