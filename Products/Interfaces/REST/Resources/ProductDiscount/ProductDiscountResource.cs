namespace Products.Interfaces.REST.Resources;

public record ProductDiscountResource(
    int Id,
    int ProductId,
    string Name,
    string DiscountType,
    decimal DiscountValue,
    DateTime StartsAt,
    DateTime EndsAt,
    bool IsActive
);