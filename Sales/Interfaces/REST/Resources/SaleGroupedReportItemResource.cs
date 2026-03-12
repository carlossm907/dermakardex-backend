namespace Sales.Interfaces.REST.Resources;

public record SaleGroupedReportItemResource(
    int SaleId,
    string TicketNumber,
    DateOnly SaleDate,
    TimeOnly SaleTime,
    decimal FinalAmount,
    IEnumerable<SaleGroupedReportProductResource> Items
);