namespace Products.Domain.Model.Commands;

public record RemoveDiscountFromProductCommand(
    int ProductId
);