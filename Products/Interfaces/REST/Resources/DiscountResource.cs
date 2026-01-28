using Products.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources;

public record DiscountResource(
    DiscountType Type,
    decimal Value
);