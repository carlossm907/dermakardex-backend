using Shared.Domain.Model.ValueObjects;

namespace dermakardex_backend.Products.Domain.Model.Commands.Product;

public record ApplyDiscountToProductCommand(
    int ProductId,
    DiscountType Type,
    decimal Value
);