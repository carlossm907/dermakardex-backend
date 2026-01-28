using Products.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands;

public record ApplyDiscountToProductCommand(
    int ProductId,
    DiscountType Type,
    decimal Value
);