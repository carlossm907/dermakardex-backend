
using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Commands;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.CommandServices;

public class ProductCommandService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductCommandService
{
    public async Task<Product?> Handle(CreateProductCommand command)
    {
        var product = new Product(command);

        await productRepository.AddAsync(product);
        await unitOfWork.CompleteAsync();

        return product;
    }

    public async Task<Product?> Handle(UpdateProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) return null;

        product.SetName(command.Name);
        product.ChangePresentation(command.Presentation);

        product.GetType().GetProperty("BrandId")?.SetValue(product, command.BrandId);
        product.GetType().GetProperty("LaboratoryId")?.SetValue(product, command.LaboratoryId);
        product.GetType().GetProperty("CategoryId")?.SetValue(product, command.CategoryId);
        product.GetType().GetProperty("SupplierId")?.SetValue(product, command.SupplierId);

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
}