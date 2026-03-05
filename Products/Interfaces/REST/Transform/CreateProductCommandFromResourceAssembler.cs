using dermakardex_backend.Products.Domain.Model.Commands.Product;
using Products.Interfaces.REST.Resources;
using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Transform;

public static class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource)
    {
        return new CreateProductCommand(
            resource.Code,
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