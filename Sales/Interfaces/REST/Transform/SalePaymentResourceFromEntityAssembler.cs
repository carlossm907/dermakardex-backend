using Sales.Domain.Model.Entities;
using Sales.Interfaces.REST.Resources;

namespace Sales.Interfaces.REST.Transform;

public static class SalePaymentResourceFromEntityAssembler
{
    public static SalePaymentResource ToResourceFromEntity(SalePayment payment)
    {
        return new SalePaymentResource(
            payment.Method.ToString(),
            payment.Amount.Amount
        );
    }
}