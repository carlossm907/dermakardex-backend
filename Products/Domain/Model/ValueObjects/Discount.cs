using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.ValueObjects;

public class Discount
{
    public DiscountType Type { get; }
    public decimal Value { get; }

    protected Discount() { }

    private Discount(DiscountType type, decimal value)
    {
        Type = type;
        Value = value;
        Validate();
    }

    public static Discount None()
    {
        return new(DiscountType.None, 0);
    }

    public static Discount Amount(decimal amount)
    {
        return new(DiscountType.Amount, amount);
    }

    public static Discount Percentaje(decimal percentage)
    {
        return new(DiscountType.Percentaje, percentage);
    }

    private void Validate()
    {
        if (Value < 0)
        {
            throw new ArgumentException("Discount canot be less than 0");
        }

        if (Type == DiscountType.Percentaje && Value > 100)
        {
            throw new ArgumentException("Percentage discount cannot exceed 100%");
        }
    }

    public Money CalculateDiscount(Money salePrice)
    {
        switch (Type)
        {
            case DiscountType.Amount:
                return new Money(Value);
            case DiscountType.Percentaje:
                return new Money(salePrice.Amount * (Value / 100m));
            default:
                return new Money(0);
        }
    }
}