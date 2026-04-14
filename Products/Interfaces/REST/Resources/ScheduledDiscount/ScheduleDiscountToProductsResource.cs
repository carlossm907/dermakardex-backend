using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources.ScheduledDiscount;

public record ScheduleDiscountToProductsResource(
    IEnumerable<int> ProductIds,
    string Name,
    DiscountType DiscountType,
    decimal DiscountValue,
    DateTime StartsAt,
    DateTime EndsAt
);