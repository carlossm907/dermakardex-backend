using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources;

public record UpdateScheduledDiscountResource(
    string Name,
    DiscountType Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);