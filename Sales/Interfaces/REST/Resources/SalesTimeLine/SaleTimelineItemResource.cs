namespace Sales.Interfaces.REST.Resources.SalesTimeLine;

public record SaleTimelineItemResource(
    int SaleId,
    string TicketNumber,
    string CustomerFullName,
    DateOnly SaleDate,
    TimeOnly SaleTime,
    decimal Total,
    List<SaleGroupedReportProductResource> Items,
    List<SalePaymentResource> Payments
);