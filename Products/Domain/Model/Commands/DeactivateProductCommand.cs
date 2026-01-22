namespace Products.Domain.Model.Commands;

public record DeactivateProductCommand(
    int ProductId
);