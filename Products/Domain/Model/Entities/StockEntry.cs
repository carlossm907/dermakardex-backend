using Products.Domain.Model.Aggregates;

namespace Products.Domain.Model.Entities;

public class StockEntry
{
    public int Id { get; private set; }

    public int ProductId { get; private set; }

    public int Quantity { get; private set; }

    public Money UnitPurchasePrice { get; private set; }

    public Money TotalInvestment { get; private set; }

    public string Reason { get; private set; }

    public int RegisteredByUserId { get; private set; }

    public DateTime RegisteredAt { get; private set; }

    protected StockEntry() { }

    public StockEntry(
        int productId,
        int quantity,
        Money unitPurchasePrice,
        string reason,
        int registeredByUserId
    )
    {
        if (productId <= 0)
        {
            throw new ArgumentException("ProductId must be valid");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero");
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException("Reason is required");
        }

        ProductId = productId;
        Quantity = quantity;
        UnitPurchasePrice = unitPurchasePrice ?? throw new ArgumentNullException(nameof(unitPurchasePrice));
        TotalInvestment = unitPurchasePrice.Multiply(quantity);
        Reason = reason.Trim();
        RegisteredByUserId = registeredByUserId;
        RegisteredAt = DateTime.UtcNow;
    }


}