using Products.Domain.Model.ValueObjects;

namespace dermakardex_backend.Products.Domain.Model.Commands.Product;

public record ApplyDiscountToAllProductsCommand(
    DiscountType Type,
    decimal Value
);