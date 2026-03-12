using Sales.Domain.Model.ReadModels;
using Sales.Interfaces.REST.Resources;

namespace Sales.Interfaces.REST.Transform;

public static class SalesGroupedByCustomerReportResourceFromModelAssembler
{
    public static SalesGroupedByCustomerReportResource ToResourceFromModel(SalesGroupedByCustomerReport model)
    {
        return new SalesGroupedByCustomerReportResource(
            model.CustomerFullName,
            model.CustomerDni,
            model.CustomerTotalAmount,
            model.Sales.Select(s => new SaleGroupedReportItemResource(
                s.SaleId,
                s.TicketNumber,
                s.SaleDate,
                s.SaleTime,
                s.FinalAmount,
                s.Items.Select(i => new SaleGroupedReportProductResource(
                    i.ProductId,
                    i.ProductName,
                    i.Presentation,
                    i.Quantity,
                    i.UnitPrice,
                    i.LineTotal
                ))
            ))
        );
    }
}