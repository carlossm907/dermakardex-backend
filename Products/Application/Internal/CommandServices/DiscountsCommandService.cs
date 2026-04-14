using dermakardex_backend.Products.Domain.Model.Commands.Product;
using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.ValueObjects;
using Products.Domain.Repositories;
using Products.Domain.Services;
using Shared.Domain.Model.ValueObjects;

namespace Products.Application.Internal.CommandServices;

public class DiscountsCommandService(

    IProductRepository productRepository,
    IUnitOfWork unitOfWork

) : IDiscountsCommandService
{
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
            if (product is null) continue;

            var disc =
                command.Type == DiscountType.AMOUNT
                    ? Discount.Amount(command.Value)
                    : command.Type == DiscountType.PERCENTAGE
                        ? Discount.Percentage(command.Value)
                        : Discount.None();

            product.SetDiscount(disc);
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
}