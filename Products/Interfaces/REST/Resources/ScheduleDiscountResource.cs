namespace Products.Interfaces.REST.Resources;

public record ScheduleDiscountResource(
    int ProductId,
    string Type,
    decimal Value,
    DateTime StartsAt,
    DateTime EndsAt
);