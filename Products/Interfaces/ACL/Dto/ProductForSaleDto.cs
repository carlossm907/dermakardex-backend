namespace Products.Interfaces.ACL.Dto;

public record ProductForSaleDto(
    int ProductId,
    string Name,
    int PresentationUnits,
    decimal BaseUnitPrice,
    decimal FinalUnitPrice,
    decimal DiscountValue,
    string DiscountType,
    int AvailableStock
);