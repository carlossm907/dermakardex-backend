using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels.SalesTimeLine;
using Sales.Domain.Repositories;
using Sales.Domain.Services;

namespace Sales.Application.Internal.QueryServices;

public class SaleBySellerQueryService(

    ISaleRepository saleRepository

) : ISaleBySellerQueryService
{

    public async Task<IEnumerable<Sale>> Handle(GetSalesBySellerUserIdQuery query)
    {
        return await saleRepository.FindBySellerUserIdAsync(query.SellerUserId);
    }

    public async Task<IEnumerable<SalesTimelineByDay>> Handle(GetSalesTimelineByMonthQuery query)
    {
        var sales = await saleRepository.FindByMonthWithDetailsAsync(query.Year, query.Month);

        var result = sales
            .OrderBy(s => s.SaleDate)
            .ThenBy(s => s.SaleTime)
            .GroupBy(s => s.SaleDate)
            .Select(dayGroup =>
            {
                var blocks = new List<SellerBlock>();
                SellerBlock? currentBlock = null;

                foreach (var sale in dayGroup)
                {
                    if (currentBlock == null || currentBlock.SellerUserId != sale.SellerUserId)
                    {
                        currentBlock = new SellerBlock
                        {
                            SellerUserId = sale.SellerUserId,
                            SellerFullName = sale.SellerFullName,
                            Sales = new List<SaleDetailItem>()
                        };

                        blocks.Add(currentBlock);
                    }

                    currentBlock.Sales.Add(new SaleDetailItem
                    {
                        SaleId = sale.Id,
                        TicketNumber = sale.TicketNumber,

                        CustomerFullName = sale.CustomerFullName,

                        SaleDate = sale.SaleDate,
                        SaleTime = sale.SaleTime,

                        Total = sale.Total.Amount,

                        Items = sale.Items.Select(i => new SaleItemDetail
                        {
                            ProductId = i.ProductId,
                            ProductName = i.ProductName,
                            Presentation = i.Presentation.ToString(),
                            Quantity = i.Quantity,
                            UnitPrice = i.UnitPrice.Amount,
                            LineTotal = i.LineTotal.Amount
                        }).ToList(),

                        Payments = sale.Payments.Select(p => new SalePaymentDetail
                        {
                            Method = p.Method.ToString(),
                            Amount = p.Amount.Amount
                        }).ToList()
                    });
                }

                return new SalesTimelineByDay
                {
                    Date = dayGroup.Key,
                    Blocks = blocks
                };
            })
            .ToList();

        return result;
    }


}