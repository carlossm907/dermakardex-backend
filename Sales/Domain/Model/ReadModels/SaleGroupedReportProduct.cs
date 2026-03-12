namespace Sales.Domain.Model.ReadModels;

public class SaleGroupedReportProduct
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Presentation { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}