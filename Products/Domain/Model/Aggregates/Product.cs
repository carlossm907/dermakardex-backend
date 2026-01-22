using Products.Domain.Model.Commands;

namespace Products.Domain.Model.Aggregates;

public class Product
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public int BrandId { get; private set; }

    public int LaboratoryId { get; private set; }

    public int CategoryId { get; private set; }

    public int SupplierId { get; private set; }

    public ProductPresentation Presentation { get; private set; }

    public Money PurchasePrice { get; private set; }

    public Money SalePrice { get; private set; }

    public Money MaxDiscountAmount { get; private set; }

    public int Stock { get; private set; }

    public int StockAlertThreshold { get; private set; }

    public bool IsActive { get; private set; }

    protected Product() { }

    public Product(string name, int brandId, int laboratoryId, int categoryId, int supplierId, ProductPresentation presentation, Money purchasePrice, Money salePrice, Money maxDiscountAmount, int inititalStock, int stockAlertThreshold)
    {
        SetName(name);

        BrandId = brandId;
        LaboratoryId = laboratoryId;
        CategoryId = categoryId;
        SupplierId = supplierId;

        Presentation = presentation;

        PurchasePrice = purchasePrice ?? throw new ArgumentNullException(nameof(purchasePrice));
        SalePrice = salePrice ?? throw new ArgumentNullException(nameof(salePrice));
        MaxDiscountAmount = maxDiscountAmount ?? throw new ArgumentNullException(nameof(maxDiscountAmount));

        SetStock(inititalStock);
        SetStockAlert(stockAlertThreshold);

        ValidateDiscountRule();

        IsActive = true;

    }

    public Product(CreateProductCommand command)
    : this(
        command.Name,
        command.BrandId,
        command.LaboratoryId,
        command.CategoryId,
        command.SupplierId,
        command.Presentation,
        new Money(command.PurchasePrice),
        new Money(command.SalePrice),
        new Money(command.MaxDiscountAmount),
        command.InitialStock,
        command.StockAlertThreshold
    )
    {

    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name cannot be empty");
        }

        Name = name.Trim();
    }

    public void ChangePresentation(ProductPresentation presentation)
    {
        Presentation = presentation;
    }

    public void ChangePrices(Money purchasePrice, Money salePrice)
    {
        PurchasePrice = purchasePrice ?? throw new ArgumentNullException(nameof(purchasePrice));
        SalePrice = salePrice ?? throw new ArgumentNullException(nameof(salePrice));
    }

    public void ChangeMaxDiscount(Money maxDiscountAmount)
    {
        MaxDiscountAmount = maxDiscountAmount ?? throw new ArgumentNullException(nameof(maxDiscountAmount));
        ValidateDiscountRule();
    }

    private void ValidateDiscountRule()
    {
        if (MaxDiscountAmount.Amount < 0)
            throw new ArgumentException("Max discount cannot be negative");

        if (MaxDiscountAmount.Amount > SalePrice.Amount)
            throw new ArgumentException("Max discount cannot exceed sale price");
    }

    public void SetStock(int stock)
    {
        if (stock < 0)
            throw new ArgumentException("Stock cannot be negative");

        Stock = stock;
    }

    public void AdjustStock(int delta)
    {
        var newStock = Stock + delta;

        if (newStock < 0)
            throw new InvalidOperationException("Insufficient stock");

        Stock = newStock;
    }

    public void SetStockAlert(int threshold)
    {
        if (threshold < 0)
            throw new ArgumentException("Stock alert threshold cannot be negative");

        StockAlertThreshold = threshold;
    }

    public bool IsLowStock()
        => Stock <= StockAlertThreshold;

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

}