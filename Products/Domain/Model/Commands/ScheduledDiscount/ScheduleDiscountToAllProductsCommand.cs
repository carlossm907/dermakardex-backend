using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands.ScheduledDiscount;

public record ScheduleDiscountToAllProductsCommand(
    string Name,
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);