using Products.Domain.Model.Aggregates;

namespace Products.Domain.Model.Commands;

public record UpdateProductCommand(
    int ProductId,
    string Name,
    ProductPresentation Presentation,
    int BrandId,
    int CategoryId,
    int SupplierId,
    int LaboratoryId
);