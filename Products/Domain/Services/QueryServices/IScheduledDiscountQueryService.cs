using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries.ScheduledDiscount;


namespace Products.Domain.Services;

public interface IScheduledDiscountQueryService
{
    Task<IEnumerable<ProductDiscount>> Handle(GetAllScheduledDiscountsQuery query);

    Task<IEnumerable<Product>> Handle(GetProductsWithScheduledDiscountsQuery query);

    Task<IEnumerable<ProductDiscount>> Handle(GetActiveScheduledDiscountsQuery query);

    Task<IEnumerable<ProductDiscount>> Handle(GetScheduledDiscountsByProductIdQuery query);

    Task<IEnumerable<ProductDiscount>> Handle(GetExpiredScheduledDiscountsQuery query);
}