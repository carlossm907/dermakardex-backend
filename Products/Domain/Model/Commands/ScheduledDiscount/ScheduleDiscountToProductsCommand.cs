using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands.ScheduledDiscount;

public record ScheduleDiscountToProductsCommand(
    IEnumerable<int> ProductIds,
    string Name,
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);