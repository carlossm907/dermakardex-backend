namespace Products.Interfaces.REST.Resources;

public record ScheduleDiscountResource(
    int ProductId,
    string Name,
    string Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);