using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels;
using Sales.Domain.Repositories;
using Sales.Domain.Services;

namespace Sales.Application.Internal.QueryServices;

public class SaleQueryService(ISaleRepository saleRepository) : ISaleQueryService
{
    public async Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query)
    {
        return await saleRepository.FindAllOrderedAsync();
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByCustomerDniQuery query)
    {
        return await saleRepository.FindByCustomerDniAsync(query.CustomerDni);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesBySellerUserIdQuery query)
    {
        return await saleRepository.FindBySellerUserIdAsync(query.SellerUserId);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByProductIdQuery query)
    {
        return await saleRepository.FindByProductIdAsync(query.ProductId);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByDayQuery query)
    {
        return await saleRepository.FindByDayAsync(query.Day);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByMonthQuery query)
    {
        return await saleRepository.FindByMonthAsync(query.Year, query.Month);
    }

    public async Task<Sale?> Handle(GetSaleByIdQuery query)
    {
        return await saleRepository.FindByIdWithDetailsAsync(query.SaleId);
    }

    public async Task<IEnumerable<SalesGroupedByCustomerReport>> Handle(GetSalesGroupedByCustomerByDayQuery query)
    {
        var sales = await saleRepository.FindByDayWithDetailsAsync(query.Day);

        var grouped = sales
            .GroupBy(s => new { s.CustomerDni, s.CustomerFullName })
            .Select(group => new SalesGroupedByCustomerReport
            {
                CustomerDni = group.Key.CustomerDni,
                CustomerFullName = group.Key.CustomerFullName,
                CustomerTotalAmount = group.Sum(x => x.Total.Amount),

                Sales = group.Select(s => new SaleGroupedReportItem
                {
                    SaleId = s.Id,
                    TicketNumber = s.TicketNumber,
                    SaleDate = s.SaleDate,
                    SaleTime = s.SaleTime,
                    FinalAmount = s.Total.Amount,

                    Items = s.Items.Select(i => new SaleGroupedReportProduct
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        Presentation = i.Presentation.ToString(),
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice.Amount,
                        LineTotal = i.LineTotal.Amount
                    }).ToList()

                }).OrderBy(x => x.SaleTime).ToList()
            })
            .OrderBy(x => x.CustomerFullName)
            .ToList();

        return grouped;
    }

    public async Task<IEnumerable<SalesGroupedByCustomerReport>> Handle(GetSalesGroupedByCustomerByMonthQuery query)
    {
        var sales = await saleRepository.FindByMonthWithDetailsAsync(query.Year, query.Month);

        var grouped = sales
            .GroupBy(s => new { s.CustomerDni, s.CustomerFullName })
            .Select(group => new SalesGroupedByCustomerReport
            {
                CustomerDni = group.Key.CustomerDni,
                CustomerFullName = group.Key.CustomerFullName,
                CustomerTotalAmount = group.Sum(x => x.Total.Amount),

                Sales = group.Select(s => new SaleGroupedReportItem
                {
                    SaleId = s.Id,
                    TicketNumber = s.TicketNumber,
                    SaleDate = s.SaleDate,
                    SaleTime = s.SaleTime,
                    FinalAmount = s.Total.Amount,

                    Items = s.Items.Select(i => new SaleGroupedReportProduct
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        Presentation = i.Presentation.ToString(),
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice.Amount,
                        LineTotal = i.LineTotal.Amount
                    }).ToList()

                })
                .OrderBy(x => x.SaleDate)
                .ThenBy(x => x.SaleTime)
                .ToList()
            })
            .OrderBy(x => x.CustomerFullName)
            .ToList();

        return grouped;
    }
}