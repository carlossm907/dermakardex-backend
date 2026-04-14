using Shared.Domain.Model.ValueObjects;

namespace dermakardex_backend.Products.Domain.Model.Commands.Discounts;

public record ApplyDiscountToAllProductsCommand(
    DiscountType Type,
    decimal Value
);