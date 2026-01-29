using Products.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands;

public record ApplyDiscountToAllProductsCommand(
    DiscountType Type,
    decimal Value
);