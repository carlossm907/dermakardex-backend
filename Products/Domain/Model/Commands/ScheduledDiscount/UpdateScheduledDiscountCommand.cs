using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands.ProductDiscount;

public record UpdateScheduledDiscountCommand(
    int ProductDiscountId,
    string Name,
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);