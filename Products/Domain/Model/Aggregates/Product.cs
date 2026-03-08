using dermakardex_backend.Products.Domain.Model.Commands.Product;
using Products.Domain.Model.Entities;
using Products.Domain.Model.ValueObjects;
using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Aggregates;

public class Product
{
    public int Id { get; private set; }

    public string Code { get; private set; }

    public string Name { get; private set; }

    public int BrandId { get; private set; }

    public int LaboratoryId { get; private set; }

    public int CategoryId { get; private set; }

    public int SupplierId { get; private set; }

    public ProductPresentation Presentation { get; private set; }

    public Money PurchasePrice { get; private set; }

    public Money SalePrice { get; private set; }

    public Money MaxDiscountAmount { get; private set; }

    public Discount Discount { get; private set; }

    public int Stock { get; private set; }

    public int StockAlertThreshold { get; private set; }

    public bool IsActive { get; private set; }

    protected Product()
    {
        Discount = Discount.None();

    }

    public Product(string code, string name, int brandId, int laboratoryId, int categoryId, int supplierId, ProductPresentation presentation, Money purchasePrice, Money salePrice, Money maxDiscountAmount, int inititalStock, int stockAlertThreshold)
    {
        SetName(name);

        Code = code;
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

        Discount = Discount.None();

        IsActive = true;

    }

    public Product(CreateProductCommand command)
    : this(
        command.Code,
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

    public void Update(
    string code,
    string name,
    int brandId,
    int laboratoryId,
    int categoryId,
    int supplierId,
    ProductPresentation presentation,
    Money purchasePrice,
    Money salePrice,
    Money maxDiscountAmount,
    int stockAlertThreshold,
    bool isActive
)
    {
        SetName(name);

        ChangeCode(code);
        ChangeBrand(brandId);
        ChangeLaboratory(laboratoryId);
        ChangeCategory(categoryId);
        ChangeSupplier(supplierId);

        ChangePresentation(presentation);
        ChangePrices(purchasePrice, salePrice);
        ChangeMaxDiscount(maxDiscountAmount);
        SetStockAlert(stockAlertThreshold);

        if (isActive)
            Activate();
        else
            Deactivate();
    }

    public void ChangeCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Product code cannot be empty");
        }

        Code = code.Trim();
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

    public void ChangeBrand(int brandId)
    {
        if (brandId <= 0)
            throw new ArgumentException("BrandId must be valid");

        BrandId = brandId;
    }

    public void ChangeCategory(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("CategoryId must be valid");

        CategoryId = categoryId;
    }

    public void ChangeSupplier(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("SupplierId must be valid");

        SupplierId = supplierId;
    }

    public void ChangeLaboratory(int laboratoryId)
    {
        if (laboratoryId <= 0)
            throw new ArgumentException("LaboratoryId must be valid");

        LaboratoryId = laboratoryId;
    }


    public void ChangePrices(Money purchasePrice, Money salePrice)
    {
        PurchasePrice = purchasePrice ?? throw new ArgumentNullException(nameof(purchasePrice));
        SalePrice = salePrice ?? throw new ArgumentNullException(nameof(salePrice));
    }

    public void SetDiscount(Discount discount)
    {
        var discountAmount = discount.CalculateDiscount(SalePrice);

        if (discountAmount.Amount > MaxDiscountAmount.Amount)
        {
            throw new ArgumentException($"Discount exceeds max allowed ({MaxDiscountAmount.Amount})");
        }

        Discount.Update(discount.Type, discount.Value);
    }

    public void ValidateDiscount(Discount discount)
    {
        var discountAmount = discount.CalculateDiscount(SalePrice);

        if (discountAmount.Amount > MaxDiscountAmount.Amount)
        {
            throw new ArgumentException($"Discount exceeds max allowed ({MaxDiscountAmount.Amount})");
        }
    }

    public void RemoveDiscount()
    {
        Discount = Discount.None();
    }

    public Money GetFinalPrice()
    {
        var discountAmount = Discount.CalculateDiscount(SalePrice);
        var finalPrice = new Money(SalePrice.Amount - discountAmount.Amount);

        return finalPrice;
    }

    public void ChangeMaxDiscount(Money maxDiscountAmount)
    {
        MaxDiscountAmount = maxDiscountAmount ?? throw new ArgumentNullException(nameof(maxDiscountAmount));
        var discountAmount = Discount.CalculateDiscount(SalePrice);

        if (MaxDiscountAmount.Amount < discountAmount.Amount)
        {
            throw new ArgumentException("Current discount exceeds new max allowed");
        }
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