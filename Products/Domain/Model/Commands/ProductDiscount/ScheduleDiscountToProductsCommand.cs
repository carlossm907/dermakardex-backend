using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands.ProductDiscount;

public record ScheduleDiscountToProductsCommand(
    List<int> ProductIds,
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);