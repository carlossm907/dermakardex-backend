using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources.ScheduledDiscount;

public record ScheduleDiscountToAllProductsResource(
    string Name,
    DiscountType DiscountType,
    decimal DiscountValue,
    DateTime StartsAt,
    DateTime EndsAt
);