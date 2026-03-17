using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Queries;
using Products.Domain.Model.Queries.SalesProductReport;
using Products.Domain.Model.ReadModels;

namespace Products.Domain.Services;


public interface IProductQueryService
{
    Task<Product?> Handle(GetProductByIdQuery query);

    Task<IEnumerable<Product>> Handle(ListProductsQuery query);

    Task<IEnumerable<Product>> Handle(GetLowStockProductsQuery query);

    Task<Product?> Handle(GetProductByCodeQuery query);

    Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetProductDailyStockReportQuery query);

    Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetAllProductsDailyStockReportQuery query);

    Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetProductsDailyStockReportQuery query);

    Task<IEnumerable<ProductSalesReportItem>> Handle(GetProductSalesReportQuery query);
    Task<IEnumerable<ProductSalesReportItem>> Handle(GetProductsSalesReportQuery query);
    Task<IEnumerable<ProductSalesReportItem>> Handle(GetAllProductsSalesReportQuery query);
}