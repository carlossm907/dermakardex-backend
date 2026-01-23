using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Commands;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource)
    {
        return new CreateProductCommand(
            resource.Name,
            resource.BrandId,
            resource.LaboratoryId,
            resource.CategoryId,
            resource.SupplierId,
            Enum.Parse<ProductPresentation>(resource.Presentation),
            resource.PurchasePrice,
            resource.SalePrice,
            resource.MaxDiscountAmount,
            resource.InitialStock,
            resource.StockAlertThreshold
        );
    }
}