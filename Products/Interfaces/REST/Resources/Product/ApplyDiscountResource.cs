using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources;

public record ApplyDiscountResource(
    DiscountType Type,
    decimal Value
);