using Products.Domain.Model.Aggregates;
using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Entities;

public class StockEntry
{
    public int Id { get; private set; }

    public int ProductId { get; private set; }

    public string ProductName { get; private set; }

    public int Quantity { get; private set; }

    public DateOnly ExpirationDate { get; private set; }

    public Money UnitPurchasePrice { get; private set; }

    public Money TotalInvestment { get; private set; }

    public string Reason { get; private set; }

    public string UserFullName { get; private set; }

    public DateTime RegisteredAt { get; private set; }

    protected StockEntry() { }

    public StockEntry(
        int productId,
        string productName,
        int quantity,
        DateOnly expirationDate,
        Money unitPurchasePrice,
        string reason,
        string userFullName
    )
    {
        if (productId <= 0)
        {
            throw new ArgumentException("ProductId must be valid");
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("ProductName is required");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero");
        }

        if (expirationDate < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Expiration date cannot be in the past.");
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException("Reason is required");
        }

        ProductId = productId;
        ProductName = productName.Trim();
        Quantity = quantity;
        ExpirationDate = expirationDate;
        UnitPurchasePrice = unitPurchasePrice ?? throw new ArgumentNullException(nameof(unitPurchasePrice));
        TotalInvestment = unitPurchasePrice.Multiply(quantity);
        Reason = reason.Trim();
        UserFullName = userFullName;
        RegisteredAt = DateTime.UtcNow;
    }


}