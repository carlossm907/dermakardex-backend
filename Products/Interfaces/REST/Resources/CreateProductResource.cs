namespace Products.Interfaces.REST.Resources;

public record CreateProductResource(
    string Name,
    int BrandId,
    int LaboratoryId,
    int CategoryId,
    int SupplierId,
    string Presentation,
    decimal PurchasePrice,
    decimal SalePrice,
    decimal MaxDiscountAmount,
    int InitialStock,
    int StockAlertThreshold
);