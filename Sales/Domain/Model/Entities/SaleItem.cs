using Products.Domain.Model.Aggregates;
using Products.Domain.Model.ValueObjects;

namespace Sales.Domain.Model.Entities;

public class SaleItem
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public string ProductName { get; private set; }
    public ProductPresentation Presentation { get; private set; }
    public int Quantity { get; private set; }

    public Money BaseUnitPrice { get; private set; }
    public Money UnitPrice { get; private set; }

    public DiscountType DiscountType { get; private set; }
    public decimal DiscountValue { get; private set; }

    public Money LineTotal { get; private set; }

    protected SaleItem()
    {
        ProductName = string.Empty;
        Presentation = ProductPresentation.Units;
        BaseUnitPrice = new Money(0);
        UnitPrice = new Money(0);
        LineTotal = new Money(0);
    }

    public SaleItem(
        int productId,
        string productName,
        ProductPresentation presentation,
        int quantity,
        Money baseUnitPrice,
        Money unitPrice,
        DiscountType discountType,
        decimal discountValue
    )
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero");
        }

        if (unitPrice.Amount > baseUnitPrice.Amount)
        {
            throw new ArgumentException("UnitPrice cannot exceed BaseUnitPrice");
        }

        if (productId <= 0)
        {
            throw new ArgumentException("ProductId must be greater than zero");
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("ProductName is required");
        }

        ProductId = productId;
        ProductName = productName.Trim();
        Presentation = presentation;

        Quantity = quantity;
        UnitPrice = unitPrice;
        BaseUnitPrice = baseUnitPrice;
        DiscountType = discountType;

        DiscountValue = discountType == DiscountType.None ? 0 : discountValue;

        LineTotal = unitPrice.Multiply(quantity);

    }
}