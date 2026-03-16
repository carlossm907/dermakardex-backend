namespace Sales.Domain.Model.ReadModels;

public class ProductSalesPerDay
{
    public int ProductId { get; set; }
    public DateOnly Date { get; set; }
    public int Quantity { get; set; }
}