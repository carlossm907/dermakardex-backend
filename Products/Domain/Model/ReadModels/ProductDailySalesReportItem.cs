namespace Products.Domain.Model.ReadModels;

public class ProductSalesReportItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public int Quantity { get; set; }
}