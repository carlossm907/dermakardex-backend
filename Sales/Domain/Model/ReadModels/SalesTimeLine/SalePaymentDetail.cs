namespace Sales.Domain.Model.ReadModels.SalesTimeLine;

public class SalePaymentDetail
{
    public string Method { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}