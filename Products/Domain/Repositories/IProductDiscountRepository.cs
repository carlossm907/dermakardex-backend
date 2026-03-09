using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;

namespace Products.Domain.Repositories;

public interface IProductDiscountRepository : IBaseRepository<ProductDiscount>
{

    Task<IEnumerable<ProductDiscount>> FindByProductIdAsync(int productId);

    Task<IEnumerable<ProductDiscount>> FindActiveDiscountsAsync();

    Task<IEnumerable<ProductDiscount>> FindExpiredDiscountsAsync();

    Task<IEnumerable<ProductDiscount>> FindAllAsync();

    Task<bool> ExistsOverlapAsync(int productId, DateTime startsAt, DateTime endsAt);

}