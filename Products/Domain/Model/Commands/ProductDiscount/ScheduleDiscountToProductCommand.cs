using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands.ProductDiscount;

public record ScheduleDiscountToProductCommand(
    int ProductId,
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);