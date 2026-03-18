namespace Products.Interfaces.REST.Resources.Product;

public record ProductEntriesReportResource(
    int ProductId,
    string ProductName,
    DateOnly From,
    DateOnly To,
    int Quantity
);