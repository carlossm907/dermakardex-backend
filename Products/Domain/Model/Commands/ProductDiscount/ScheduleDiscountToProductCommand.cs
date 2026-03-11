using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands.ProductDiscount;

public record ScheduleDiscountToProductCommand(
    int ProductId,
    string Name,
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);