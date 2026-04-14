using dermakardex_backend.Products.Domain.Model.Commands.Discounts;

namespace Products.Domain.Services;

public interface IDiscountsCommandService
{
    Task Handle(ApplyDiscountToProductCommand command);

    Task Handle(RemoveDiscountFromAllProductsCommand command);

    Task Handle(RemoveDiscountFromProductCommand command);

    Task Handle(ApplyDiscountToProductsCommand command);

    Task Handle(ApplyDiscountToAllProductsCommand command);
}