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

    public Money Multiply(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        return new Money(Amount * quantity, Currency);
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    private void EnsureSameCurrency(Money other)
    {
        if (!Currency.Equals(other.Currency, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Currencies must match.");
    }

}