using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Queries;

namespace Products.Domain.Services;


public interface IProductQueryService
{
    Task<Product?> Handle(GetProductByIdQuery query);

    Task<IEnumerable<Product>> Handle(ListProductsQuery query);

    Task<IEnumerable<Product>> Handle(GetLowStockProductsQuery query);

    Task<Product?> Handle(GetProductByCodeQuery query);
}