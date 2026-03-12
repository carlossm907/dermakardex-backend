namespace Sales.Domain.Model.ReadModels;

public class SaleGroupedReportItem
{
    public int SaleId { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public DateOnly SaleDate { get; set; }
    public TimeOnly SaleTime { get; set; }
    public decimal FinalAmount { get; set; }
    public List<SaleGroupedReportProduct> Items { get; set; } = [];
}