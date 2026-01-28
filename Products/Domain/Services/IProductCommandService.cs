using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Commands;

namespace Products.Domain.Services;

public interface IProductCommandService
{
    Task<Product?> Handle(CreateProductCommand command);

    Task<Product?> Handle(UpdateProductCommand command);

    Task Handle(ChangeProductPricesCommand command);

    Task Handle(ChangeProductMaxDiscountCommand command);

    Task Handle(AdjustProductStockCommand command);

    Task Handle(ActivateProductCommand command);

    Task Handle(DeactivateProductCommand command);

    Task Handle(RegisterProductEntryCommand command);

}