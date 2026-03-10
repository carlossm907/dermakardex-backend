using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.ACL.Dto;

public record ProductForSaleDto(
    int ProductId,
    string Name,
    int PresentationUnits,
    decimal BaseUnitPrice,
    decimal FinalUnitPrice,
    DiscountType DiscountType,
    decimal DiscountValue,
    int AvailableStock
);