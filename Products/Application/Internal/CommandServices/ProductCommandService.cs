using dermakardex_backend.Products.Domain.Model.Commands.Product;
using dermakardex_backend.Products.Domain.Model.Commands.StockEntry;
using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Entities;
using Products.Domain.Model.ValueObjects;
using Products.Domain.Repositories;
using Products.Domain.Services;
using Shared.Domain.Model.ValueObjects;

namespace Products.Application.Internal.CommandServices;

public class ProductCommandService(
    IProductRepository productRepository,
    IBrandRepository brandRepository,
    ICategoryRepository categoryRepository,
    ISupplierRepository supplierRepository,
    ILaboratoryRepository laboratoryRepository,
    IStockEntryRepository stockEntryRepository,
    IUnitOfWork unitOfWork) : IProductCommandService
{
    public async Task<Product?> Handle(CreateProductCommand command)
    {
        await ValidateRelatedAggregatesExist(
            command.BrandId,
            command.CategoryId,
            command.SupplierId,
            command.LaboratoryId
        );

        var product = new Product(command);

        await productRepository.AddAsync(product);
        await unitOfWork.CompleteAsync();

        return product;
    }

    public async Task<Product?> Handle(UpdateProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) return null;

        product.Update(
        command.Name,
        command.BrandId,
        command.LaboratoryId,
        command.CategoryId,
        command.SupplierId,
        command.Presentation,
        new Money(command.PurchasePrice),
        new Money(command.SalePrice),
        new Money(command.MaxDiscountAmount),
        command.StockAlertThreshold,
        command.IsActive
    );

        await unitOfWork.CompleteAsync();
        return product;
    }

    public async Task Handle(ChangeProductPricesCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) return;

        product.ChangePrices(
            new Money(command.PurchasePrice),
            new Money(command.SalePrice)
        );

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(ChangeProductMaxDiscountCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) return;

        product.ChangeMaxDiscount(new Money(command.MaxDiscountAmount));
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(AdjustProductStockCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) return;

        product.AdjustStock(command.Delta);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(ActivateProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) return;

        product.Activate();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeactivateProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) return;

        product.Deactivate();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(RegisterProductEntryCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) throw new ArgumentException("Product does not exist");

        product.AdjustStock(command.Quantity);

        product.ChangePrices(
            new Money(command.UnitPurchasePrice),
            product.SalePrice
        );

        var stockEntry = new StockEntry(
        product.Id,
        command.Quantity,
        new Money(command.UnitPurchasePrice),
        command.Reason,
        command.RegisteredByUserId
    );

        await stockEntryRepository.AddAsync(stockEntry);

        await unitOfWork.CompleteAsync();

    }

    public async Task Handle(ApplyDiscountToProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);

        if (product is null) throw new ArgumentException("Product does not exist");

        Discount discount;

        if (command.Type == DiscountType.AMOUNT)
        {
            discount = Discount.Amount(command.Value);
        }
        else if (command.Type == DiscountType.PERCENTAGE)
        {
            discount = Discount.Percentage(command.Value);
        }
        else
        {
            discount = Discount.None();
        }

        product.SetDiscount(discount);

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(RemoveDiscountFromProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);

        if (product is null) throw new ArgumentException("Product does not exist");

        product.RemoveDiscount();

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(ApplyDiscountToProductsCommand command)
    {
        Discount discount;

        if (command.Type == DiscountType.AMOUNT)
        {
            discount = Discount.Amount(command.Value);
        }
        else if (command.Type == DiscountType.PERCENTAGE)
        {
            discount = Discount.Percentage(command.Value);
        }
        else
        {
            discount = Discount.None();
        }

        foreach (var productId in command.ProductIds)
        {
            var product = await productRepository.FindByIdAsync(productId);
            if (product is null)
                continue;

            product.SetDiscount(discount);
        }

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(ApplyDiscountToAllProductsCommand command)
    {
        var products = await productRepository.ListAsync();

        foreach (var product in products)
        {
            Discount discount;

            if (command.Type == DiscountType.AMOUNT)
            {
                discount = Discount.Amount(command.Value);
            }
            else if (command.Type == DiscountType.PERCENTAGE)
            {
                discount = Discount.Percentage(command.Value);
            }
            else
            {
                discount = Discount.None();
            }

            product.SetDiscount(discount);
        }

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(RemoveDiscountFromAllProductsCommand command)
    {
        var products = await productRepository.ListAsync();

        foreach (var product in products)
        {
            product.RemoveDiscount();
        }

        await unitOfWork.CompleteAsync();
    }

    private async Task ValidateRelatedAggregatesExist(
        int brandId,
        int categoryId,
        int supplierId,
        int laboratoryId
    )
    {
        if (!await brandRepository.ExistsByIdAsync(brandId))
            throw new ArgumentException("Brand does not exist");

        if (!await categoryRepository.ExistsByIdAsync(categoryId))
            throw new ArgumentException("Category does not exist");

        if (!await supplierRepository.ExistsByIdAsync(supplierId))
            throw new ArgumentException("Supplier does not exist");

        if (!await laboratoryRepository.ExistsByIdAsync(laboratoryId))
            throw new ArgumentException("Laboratory does not exist");
    }
}