namespace Products.Interfaces.REST.Resources;

public record ProductDiscountResource(
    int Id,
    int ProductId,
    string DiscountType,
    decimal DiscountValue,
    DateTime StartsAt,
    DateTime EndsAt,
    bool IsActive
);