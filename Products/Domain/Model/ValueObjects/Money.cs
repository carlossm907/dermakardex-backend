namespace Products.Domain.Model.Aggregates;

public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    protected Money() { }

    public Money(decimal amount, string currency = "PEN")
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        Amount = decimal.Round(amount, 2);
        Currency = currency;
    }
}