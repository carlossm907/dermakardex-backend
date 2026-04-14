using Products.Domain.Model.Queries.SalesProductReport;
using Products.Domain.Model.ReadModels;
using Products.Domain.Services.QueryServices;
using Products.Infrastructure.Persistence.EFC.Repositories;
using Sales.Interfaces.ACL;

namespace Products.Application.Internal.QueryServices;

public class SalesProductReportQueryService(

    ProductRepository productRepository,
    ISalesContextFacade salesContextFacade

) : ISalesProductReportQueryService
{
    public async Task<IEnumerable<ProductSalesReportItem>> Handle(GetProductSalesReportQuery query)
    {
        var product = await productRepository.FindByIdAsync(query.ProductId);

        if (product is null)
            return [];

        var salesPerDay =
            await salesContextFacade.FetchProductSalesPerDay(
                query.ProductId,
                query.From,
                query.To
            );

        var salesDictionary = salesPerDay.ToDictionary(s => s.Date, s => s.Quantity);

        bool singleDay = query.From == query.To;

        if (singleDay)
        {
            var sold = salesDictionary.GetValueOrDefault(query.From, 0);

            return
            [
                new ProductSalesReportItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                From = query.From,
                To = query.To,
                Quantity = sold
            }
            ];
        }

        return
        [
            new ProductSalesReportItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            From = query.From,
            To = query.To,
            Quantity = salesDictionary.Values.Sum()
        }
        ];
    }

    public async Task<IEnumerable<ProductSalesReportItem>> Handle(GetProductsSalesReportQuery query)
    {
        var products = (await productRepository.ListAsync(null))
        .Where(p => query.ProductIds.Contains(p.Id))
        .ToList();

        var sales =
            await salesContextFacade.FetchProductsSalesPerDay(
                query.From,
                query.To
            );

        var salesByProduct = sales
            .GroupBy(s => s.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(x => x.Date, x => x.Quantity)
            );

        var results = new List<ProductSalesReportItem>();

        bool singleDay = query.From == query.To;

        foreach (var product in products)
        {
            salesByProduct.TryGetValue(product.Id, out var productSales);
            productSales ??= new Dictionary<DateOnly, int>();

            if (singleDay)
            {
                results.Add(new ProductSalesReportItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    From = query.From,
                    To = query.To,
                    Quantity = productSales.GetValueOrDefault(query.From, 0)
                });
            }
            else
            {
                results.Add(new ProductSalesReportItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    From = query.From,
                    To = query.To,
                    Quantity = productSales.Values.Sum()
                });
            }
        }

        return results;
    }

    public async Task<IEnumerable<ProductSalesReportItem>> Handle(GetAllProductsSalesReportQuery query)
    {
        var products = await productRepository.ListAsync(null);

        var sales =
            await salesContextFacade.FetchProductsSalesPerDay(
                query.From,
                query.To
            );

        var salesByProduct = sales
            .GroupBy(s => s.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(x => x.Date, x => x.Quantity)
            );

        var results = new List<ProductSalesReportItem>();

        bool singleDay = query.From == query.To;

        foreach (var product in products)
        {
            salesByProduct.TryGetValue(product.Id, out var productSales);
            productSales ??= new Dictionary<DateOnly, int>();

            if (singleDay)
            {
                results.Add(new ProductSalesReportItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    From = query.From,
                    To = query.To,
                    Quantity = productSales.GetValueOrDefault(query.From, 0)
                });
            }
            else
            {
                results.Add(new ProductSalesReportItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    From = query.From,
                    To = query.To,
                    Quantity = productSales.Values.Sum()
                });
            }
        }

        return results;
    }

    public async Task<IEnumerable<ProductSalesReportItem>> Handle(GetAffectedProductsSalesReportQuery query)
    {
        var all = await Handle(
        new GetAllProductsSalesReportQuery(query.From, query.To)
    );

        return all.Where(r => r.Quantity > 0);
    }
}