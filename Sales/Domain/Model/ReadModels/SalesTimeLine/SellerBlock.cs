namespace Sales.Domain.Model.ReadModels.SalesTimeLine;

public class SellerBlock
{
    public int SellerUserId { get; set; }
    public string SellerFullName { get; set; } = string.Empty;

    public List<SaleDetailItem> Sales { get; set; } = [];
}