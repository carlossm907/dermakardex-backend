namespace Products.Interfaces.REST.Resources;

public record UpdateScheduledDiscountResource(
    string Name,
    string Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);