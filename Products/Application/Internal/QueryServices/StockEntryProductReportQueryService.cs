using Products.Domain.Model.Queries.StockEntryProductReport;
using Products.Domain.Model.ReadModels;
using Products.Domain.Repositories;
using Products.Domain.Services.QueryServices;

namespace Products.Application.Internal.QueryServices;

public class StockEntryProductReportQueryService(

    IProductRepository productRepository,
    IStockEntryRepository stockEntryRepository

) : IStockEntryProductReportQueryService
{
    public async Task<IEnumerable<ProductDailyEntriesReportItem>> Handle(GetProductEntriesReportQuery query)
    {
        var product = await productRepository.FindByIdAsync(query.ProductId);

        if (product is null)
            return [];

        var entries =
            await stockEntryRepository.FindProductEntriesPerDayAsync(
                query.ProductId,
                query.From,
                query.To
            );

        var dictionary = entries.ToDictionary(e => e.Date, e => e.Quantity);

        bool singleDay = query.From == query.To;

        return
        [
            new ProductDailyEntriesReportItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            From = query.From,
            To = query.To,
            Quantity = singleDay
                ? dictionary.GetValueOrDefault(query.From, 0)
                : dictionary.Values.Sum()
        }
        ];
    }

    public async Task<IEnumerable<ProductDailyEntriesReportItem>> Handle(GetProductsEntriesReportQuery query)
    {
        var products = (await productRepository.ListAsync(null))
        .Where(p => query.ProductIds.Contains(p.Id))
        .ToList();

        var entries =
            await stockEntryRepository.FindProductsEntriesPerDayAsync(
                query.From,
                query.To
            );

        var entriesByProduct = entries
            .GroupBy(e => e.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(x => x.Date, x => x.Quantity)
            );

        var results = new List<ProductDailyEntriesReportItem>();

        bool singleDay = query.From == query.To;

        foreach (var product in products)
        {
            entriesByProduct.TryGetValue(product.Id, out var productEntries);
            productEntries ??= new Dictionary<DateOnly, int>();

            results.Add(new ProductDailyEntriesReportItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                From = query.From,
                To = query.To,
                Quantity = singleDay
                    ? productEntries.GetValueOrDefault(query.From, 0)
                    : productEntries.Values.Sum()
            });
        }

        return results;
    }

    public async Task<IEnumerable<ProductDailyEntriesReportItem>> Handle(GetAllProductsEntriesReportQuery query)
    {
        var products = await productRepository.ListAsync(null);

        var entries =
            await stockEntryRepository.FindProductsEntriesPerDayAsync(
                query.From,
                query.To
            );

        var entriesByProduct = entries
            .GroupBy(e => e.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(x => x.Date, x => x.Quantity)
            );

        var results = new List<ProductDailyEntriesReportItem>();

        bool singleDay = query.From == query.To;

        foreach (var product in products)
        {
            entriesByProduct.TryGetValue(product.Id, out var productEntries);
            productEntries ??= new Dictionary<DateOnly, int>();

            results.Add(new ProductDailyEntriesReportItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                From = query.From,
                To = query.To,
                Quantity = singleDay
                    ? productEntries.GetValueOrDefault(query.From, 0)
                    : productEntries.Values.Sum()
            });
        }

        return results;
    }

    public async Task<IEnumerable<ProductDailyEntriesReportItem>> Handle(GetAffectedProductsEntriesReportQuery query)
    {
        var all = await Handle(
        new GetAllProductsEntriesReportQuery(query.From, query.To)
    );

        return all.Where(r => r.Quantity > 0);
    }
}