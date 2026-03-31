namespace Sales.Interfaces.REST.Resources.SalesTimeLine;

public record SalesTimelineByDayResource(
    DateOnly Date,
    List<SellerBlockResource> Blocks
);