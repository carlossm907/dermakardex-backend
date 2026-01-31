using Products.Domain.Model.ValueObjects;
using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Resources;

public record DiscountResource(
    DiscountType Type,
    decimal Value
);