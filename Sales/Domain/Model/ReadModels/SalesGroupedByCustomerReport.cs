namespace Sales.Domain.Model.ReadModels;

public class SalesGroupedByCustomerReport
{
    public string CustomerFullName { get; set; } = string.Empty;
    public string CustomerDni { get; set; } = string.Empty;
    public decimal CustomerTotalAmount { get; set; }
    public List<SaleGroupedReportItem> Sales { get; set; } = [];
}