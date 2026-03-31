namespace Sales.Domain.Model.ReadModels.SalesTimeLine;

public class SaleDetailItem
{
    public int SaleId { get; set; }
    public string TicketNumber { get; set; } = string.Empty;

    public string CustomerFullName { get; set; } = string.Empty;

    public DateOnly SaleDate { get; set; }
    public TimeOnly SaleTime { get; set; }

    public decimal Total { get; set; }

    public List<SaleItemDetail> Items { get; set; } = [];
    public List<SalePaymentDetail> Payments { get; set; } = [];
}