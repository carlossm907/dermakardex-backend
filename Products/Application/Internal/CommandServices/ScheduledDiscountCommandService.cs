using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Commands.ProductDiscount;
using Products.Domain.Model.Entities;
using Products.Domain.Model.ValueObjects;
using Products.Domain.Repositories;
using Products.Domain.Services;
using Shared.Domain.Model.ValueObjects;

namespace Products.Application.Internal.CommandServices;

public class ScheduledDiscountCommandService(
    IProductRepository productRepository,
    IProductDiscountRepository productDiscountRepository,
    IUnitOfWork unitOfWork
) : IScheduledDiscountCommandService
{
    public async Task<ProductDiscount> Handle(ScheduleDiscountToProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if (product is null) throw new ArgumentException("Product does not exist");

        if (command.EndsAt <= command.StartsAt) throw new ArgumentException("End date must be greater than start date");

        var discount = CreateDiscount(command.Type, command.Value);

        var productDiscount = new ProductDiscount(
            command.ProductId,
            command.Name,
            discount,
            command.StartsAt,
            command.EndsAt
        );

        await productDiscountRepository.AddAsync(productDiscount);

        await unitOfWork.CompleteAsync();

        return productDiscount;
    }

    public async Task Handle(ScheduleDiscountToProductsCommand command)
    {
        foreach (var productId in command.ProductIds)
        {
            var product = await productRepository.FindByIdAsync(productId);
            if (product is null) continue;

            var discount = CreateDiscount(command.Type, command.Value);

            var productDiscount = new ProductDiscount(
                productId,
                command.Name,
                discount,
                command.StartsAt,
                command.EndsAt
            );

            await productDiscountRepository.AddAsync(productDiscount);
        }

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(ScheduleDiscountToAllProductsCommand command)
    {
        var products = await productRepository.ListAsync();

        foreach (var product in products)
        {

            var discount = CreateDiscount(command.Type, command.Value);

            var productDiscount = new ProductDiscount(
                product.Id,
                command.Name,
                discount,
                command.StartsAt,
                command.EndsAt
            );

            await productDiscountRepository.AddAsync(productDiscount);
        }

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(UpdateScheduledDiscountCommand command)
    {
        var discount = await productDiscountRepository.FindByIdAsync(command.ProductDiscountId);

        if (discount is null) throw new ArgumentException("Scheduled discount not found");

        var newDiscount = CreateDiscount(command.Type, command.Value);

        discount.Update(command.Name, newDiscount, command.StartsAt, command.EndsAt);

        productDiscountRepository.Update(discount);

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteScheduledDiscountCommand command)
    {
        var discount = await productDiscountRepository.FindByIdAsync(command.ProductDiscountId);

        if (discount is null)
            return;

        productDiscountRepository.Remove(discount);

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(CleanupExpiredProductDiscountsCommand command)
    {
        var expiredDiscounts = await productDiscountRepository.FindExpiredDiscountsAsync();

        foreach (var discount in expiredDiscounts)
        {
            discount.Disable();
        }

        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DisableScheduledDiscountCommand command)
    {
        var discount = await productDiscountRepository.FindByIdAsync(command.ProductDiscountId);

        if (discount is null)
            return;

        discount.Disable();

        productDiscountRepository.Update(discount);

        await unitOfWork.CompleteAsync();
    }

    private Discount CreateDiscount(DiscountType type, decimal value)
    {
        switch (type)
        {
            case DiscountType.AMOUNT:
                return Discount.Amount(value);

            case DiscountType.PERCENTAGE:
                return Discount.Percentage(value);

            default:
                return Discount.None();
        }
    }
}