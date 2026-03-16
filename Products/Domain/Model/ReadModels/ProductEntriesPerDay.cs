namespace Products.Domain.Model.ReadModels;

public class ProductEntriesPerDay
{
    public int ProductId { get; set; }
    public DateOnly Date { get; set; }

    public int Quantity { get; set; }
}