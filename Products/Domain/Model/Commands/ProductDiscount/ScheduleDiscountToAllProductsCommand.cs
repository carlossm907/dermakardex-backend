using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands.ProductDiscount;

public record ScheduleDiscountToAllProductsCommand(
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);