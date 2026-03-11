using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries;

namespace Products.Domain.Services;

public interface IProductDiscountQueryService
{
    Task<IEnumerable<ProductDiscount>> Handle(GetAllScheduledDiscountsQuery query);

    Task<IEnumerable<Product>> Handle(GetProductsWithScheduledDiscountsQuery query);

    Task<IEnumerable<ProductDiscount>> Handle(GetActiveScheduledDiscountsQuery query);

    Task<IEnumerable<ProductDiscount>> Handle(GetScheduledDiscountsByProductIdQuery query);

    Task<IEnumerable<ProductDiscount>> Handle(GetExpiredScheduledDiscountsQuery query);
}