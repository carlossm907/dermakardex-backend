using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.ValueObjects;

public class Discount
{
    public DiscountType Type { get; private set; }
    public decimal Value { get; private set; }

    protected Discount() { }

    private Discount(DiscountType type, decimal value)
    {
        Type = type;
        Value = value;
        Validate();
    }

    public static Discount None()
    {
        return new(DiscountType.NONE, 0);
    }

    public static Discount Amount(decimal amount)
    {
        return new(DiscountType.AMOUNT, amount);
    }

    public static Discount Percentage(decimal percentage)
    {
        return new(DiscountType.PERCENTAGE, percentage);
    }

    private void Validate()
    {
        if (Value < 0)
        {
            throw new ArgumentException("Discount canot be less than 0");
        }

        if (Type == DiscountType.PERCENTAGE && Value > 100)
        {
            throw new ArgumentException("Percentage discount cannot exceed 100%");
        }
    }

    public Money CalculateDiscount(Money salePrice)
    {
        switch (Type)
        {
            case DiscountType.AMOUNT:
                return new Money(Value);
            case DiscountType.PERCENTAGE:
                return new Money(salePrice.Amount * (Value / 100m));
            default:
                return new Money(0);
        }
    }
}