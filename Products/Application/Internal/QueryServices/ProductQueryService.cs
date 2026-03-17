using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Queries;
using Products.Domain.Services;
using Products.Domain.Repositories;
using Products.Domain.Model.ReadModels;
using Sales.Interfaces.ACL;
using Products.Domain.Model.Queries.SalesProductReport;

namespace Products.Application.Internal.QueryServices;

public class ProductQueryService(IProductRepository productRepository, ISalesContextFacade salesContextFacade, IStockEntryRepository stockEntryRepository) : IProductQueryService
{
    public async Task<Product?> Handle(GetProductByIdQuery query)
    {
        return await productRepository.FindByIdAsync(query.ProductId);
    }

    public async Task<IEnumerable<Product>> Handle(ListProductsQuery query)
    {
        return await productRepository.ListAsync(query.Name);
    }

    public async Task<IEnumerable<Product>> Handle(GetLowStockProductsQuery query)
    {
        return await productRepository.FindLowStockAsync();
    }

    public async Task<Product?> Handle(GetProductByCodeQuery query)
    {
        return await productRepository.FindByCodeAsync(query.Code);
    }

    public async Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetProductDailyStockReportQuery query)
    {
        var product = await productRepository.FindByIdAsync(query.ProductId);

        if (product is null)
            return [];

        var entriesPerDay =
            await stockEntryRepository.FindProductEntriesPerDayAsync(
                query.ProductId,
                query.From,
                query.To
            );

        var salesPerDay =
            await salesContextFacade.FetchProductSalesPerDay(
                query.ProductId,
                query.From,
                query.To
            );

        var entriesDictionary = entriesPerDay.ToDictionary(e => e.Date, e => e.Quantity);
        var salesDictionary = salesPerDay.ToDictionary(s => s.Date, s => s.Quantity);

        int totalEntries = entriesDictionary.Values.Sum();
        int totalSold = salesDictionary.Values.Sum();

        int finalStock = product.Stock;
        int initialStock = finalStock - totalEntries + totalSold;

        if (query.From == query.To)
        {
            var entries = entriesDictionary.GetValueOrDefault(query.From, 0);
            var sold = salesDictionary.GetValueOrDefault(query.From, 0);

            return
            [
                new ProductDailyStockReportItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Date = query.From,
                InitialStock = finalStock - entries + sold,
                Entries = entries,
                Sold = sold,
                FinalStock = finalStock
            }
            ];
        }

        return
        [
            new ProductDailyStockReportItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Date = query.From,
            InitialStock = initialStock,
            Entries = totalEntries,
            Sold = totalSold,
            FinalStock = finalStock
        }
        ];
    }

    public async Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetAllProductsDailyStockReportQuery query)
    {
        var products = await productRepository.ListAsync(null);

        var entries =
            await stockEntryRepository.FindProductsEntriesPerDayAsync(
                query.From,
                query.To
            );

        var sales =
            await salesContextFacade.FetchProductsSalesPerDay(
                query.From,
                query.To
            );

        var entriesByProduct = entries
            .GroupBy(e => e.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(x => x.Date, x => x.Quantity)
            );

        var salesByProduct = sales
            .GroupBy(s => s.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(x => x.Date, x => x.Quantity)
            );

        var results = new List<ProductDailyStockReportItem>();

        bool singleDay = query.From == query.To;

        foreach (var product in products)
        {
            entriesByProduct.TryGetValue(product.Id, out var productEntries);
            salesByProduct.TryGetValue(product.Id, out var productSales);

            productEntries ??= new Dictionary<DateOnly, int>();
            productSales ??= new Dictionary<DateOnly, int>();

            int totalEntries = productEntries.Values.Sum();
            int totalSold = productSales.Values.Sum();

            int finalStock = product.Stock;
            int initialStock = finalStock - totalEntries + totalSold;

            if (singleDay)
            {
                var entriesDay = productEntries.GetValueOrDefault(query.From, 0);
                var soldDay = productSales.GetValueOrDefault(query.From, 0);

                results.Add(new ProductDailyStockReportItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Date = query.From,
                    InitialStock = finalStock - entriesDay + soldDay,
                    Entries = entriesDay,
                    Sold = soldDay,
                    FinalStock = finalStock
                });
            }
            else
            {
                results.Add(new ProductDailyStockReportItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Date = query.From,
                    InitialStock = initialStock,
                    Entries = totalEntries,
                    Sold = totalSold,
                    FinalStock = finalStock
                });
            }
        }

        return results;
    }

    public async Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetProductsDailyStockReportQuery query)
    {
        var products = (await productRepository.ListAsync(null))
        .Where(p => query.ProductIds.Contains(p.Id))
        .ToList();

        var entries =
            await stockEntryRepository.FindProductsEntriesPerDayAsync(
                query.From,
                query.To
            );

        var sales =
            await salesContextFacade.FetchProductsSalesPerDay(
                query.From,
                query.To
            );

        var entriesByProduct = entries
            .GroupBy(e => e.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(x => x.Date, x => x.Quantity)
            );

        var salesByProduct = sales
            .GroupBy(s => s.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(x => x.Date, x => x.Quantity)
            );

        var results = new List<ProductDailyStockReportItem>();

        bool singleDay = query.From == query.To;

        foreach (var product in products)
        {
            entriesByProduct.TryGetValue(product.Id, out var productEntries);
            salesByProduct.TryGetValue(product.Id, out var productSales);

            productEntries ??= new Dictionary<DateOnly, int>();
            productSales ??= new Dictionary<DateOnly, int>();

            int totalEntries = productEntries.Values.Sum();
            int totalSold = productSales.Values.Sum();

            int finalStock = product.Stock;
            int initialStock = finalStock - totalEntries + totalSold;

            if (singleDay)
            {
                var entriesDay = productEntries.GetValueOrDefault(query.From, 0);
                var soldDay = productSales.GetValueOrDefault(query.From, 0);

                results.Add(new ProductDailyStockReportItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Date = query.From,
                    InitialStock = finalStock - entriesDay + soldDay,
                    Entries = entriesDay,
                    Sold = soldDay,
                    FinalStock = finalStock
                });
            }
            else
            {
                results.Add(new ProductDailyStockReportItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Date = query.From,
                    InitialStock = initialStock,
                    Entries = totalEntries,
                    Sold = totalSold,
                    FinalStock = finalStock
                });
            }
        }

        return results;
    }

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
}
