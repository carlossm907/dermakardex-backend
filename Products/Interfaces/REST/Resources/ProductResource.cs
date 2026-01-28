using Products.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources;

public record ProductResource(
    int Id,
    string Name,
    int BrandId,
    int CategoryId,
    int LaboratoryId,
    int SupplierId,
    string Presentation,
    decimal PurchasePrice,
    decimal FinalPrice,
    decimal SalePrice,
    decimal MaxDiscountAmount,
    DiscountType DiscountType,
    decimal DiscountValue,
    int Stock,
    bool IsActive
);