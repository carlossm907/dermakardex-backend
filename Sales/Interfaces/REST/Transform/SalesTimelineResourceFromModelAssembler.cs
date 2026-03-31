namespace Sales.Interfaces.REST.Transform;

using Sales.Domain.Model.ReadModels;
using Sales.Domain.Model.ReadModels.SalesTimeLine;
using Sales.Interfaces.REST.Resources;
using Sales.Interfaces.REST.Resources.SalesTimeLine;

public static class SalesTimelineResourceFromModelAssembler
{
    public static SalesTimelineByDayResource ToResourceFromModel(SalesTimelineByDay model)
    {
        return new SalesTimelineByDayResource(
            model.Date,
            model.Blocks.Select(block => new SellerBlockResource(
                block.SellerUserId,
                block.SellerFullName,
                block.Sales.Select(sale => new SaleTimelineItemResource(
                    sale.SaleId,
                    sale.TicketNumber,
                    sale.CustomerFullName,
                    sale.SaleDate,
                    sale.SaleTime,
                    sale.Total,

                    sale.Items.Select(i => new SaleGroupedReportProductResource(
                        i.ProductId,
                        i.ProductName,
                        i.Presentation,
                        i.Quantity,
                        i.UnitPrice,
                        i.LineTotal
                    )).ToList(),

                    sale.Payments.Select(p => new SalePaymentResource(
                        p.Method,
                        p.Amount
                    )).ToList()
                )).ToList()
            )).ToList()
        );
    }
}