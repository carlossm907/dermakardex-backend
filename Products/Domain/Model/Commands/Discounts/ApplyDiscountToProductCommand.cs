using Shared.Domain.Model.ValueObjects;

namespace dermakardex_backend.Products.Domain.Model.Commands.Discounts;

public record ApplyDiscountToProductCommand(
    int ProductId,
    DiscountType Type,
    decimal Value
);