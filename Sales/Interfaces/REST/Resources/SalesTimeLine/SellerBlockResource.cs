namespace Sales.Interfaces.REST.Resources.SalesTimeLine;

public record SellerBlockResource(
    int SellerUserId,
    string SellerFullName,
    List<SaleTimelineItemResource> Sales
);