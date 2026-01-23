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
    decimal SalePrice,
    decimal MaxDiscountAmount,
    int Stock,
    bool IsActive
);