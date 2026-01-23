namespace Products.Interfaces.REST.Resources;

public record UpdateProductResource(
    string Name,
    int BrandId,
    int LaboratoryId,
    int CategoryId,
    int SupplierId,
    string Presentation,
    decimal PurchasePrice,
    decimal SalePrice,
    decimal MaxDiscountAmount,
    int StockAlertThreshold,
    bool IsActive
);