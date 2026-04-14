using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources.ScheduledDiscount;

public record ProductDiscountResource(
    int Id,
    int ProductId,
    string Name,
    DiscountType DiscountType,
    decimal DiscountValue,
    DateTime StartsAt,
    DateTime EndsAt,
    bool IsActive
);