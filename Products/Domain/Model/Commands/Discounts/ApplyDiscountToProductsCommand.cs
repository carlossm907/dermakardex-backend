using Shared.Domain.Model.ValueObjects;

namespace dermakardex_backend.Products.Domain.Model.Commands.Discounts;

public record ApplyDiscountToProductsCommand(
    IReadOnlyCollection<int> ProductIds,
    DiscountType Type,
    decimal Value
);