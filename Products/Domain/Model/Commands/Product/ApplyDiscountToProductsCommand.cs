using Products.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Commands;

public record ApplyDiscountToProductsCommand(
    IReadOnlyCollection<int> ProductIds,
    DiscountType Type,
    decimal Value
);