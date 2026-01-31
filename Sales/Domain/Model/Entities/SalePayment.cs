using Products.Domain.Model.Aggregates;
using Sales.Domain.Model.ValueObjects;
using Shared.Domain.Model.ValueObjects;

namespace Sales.Domain.Model.Entities;

public class SalePayment
{
    public int Id { get; private set; }
    public PaymentMethod Method { get; private set; }
    public Money Amount { get; private set; }

    protected SalePayment()
    {
        Amount = new Money(0);
    }

    public SalePayment(
        PaymentMethod method,
        Money amount
    )
    {
        if (amount.Amount <= 0)
        {
            throw new ArgumentException("Payment must be greater than zero");
        }

        Method = method;
        Amount = amount;
    }

}