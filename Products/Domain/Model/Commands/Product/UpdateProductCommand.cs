using Products.Domain.Model.Aggregates;

namespace dermakardex_backend.Products.Domain.Model.Commands.Product;

public record UpdateProductCommand(
    int ProductId,
    string Name,
    int BrandId,
    int LaboratoryId,
    int CategoryId,
    int SupplierId,
    ProductPresentation Presentation,
    decimal PurchasePrice,
    decimal SalePrice,
    decimal MaxDiscountAmount,
    int StockAlertThreshold,
    bool IsActive
);
