namespace Sales.Interfaces.REST.Resources;

public record SalesGroupedByCustomerReportResource(
    string CustomerFullName,
    string CustomerDni,
    decimal CustomerTotalAmount,
    IEnumerable<SaleGroupedReportItemResource> Sales
);