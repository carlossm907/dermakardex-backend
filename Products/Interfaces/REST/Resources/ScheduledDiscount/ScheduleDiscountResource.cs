using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources.ScheduledDiscount;

public record ScheduleDiscountResource(
    int ProductId,
    string Name,
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);