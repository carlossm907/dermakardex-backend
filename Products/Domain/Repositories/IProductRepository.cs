using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Aggregates;

namespace Products.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> FindByIdAsync(int productId);

    Task<IEnumerable<Product>> ListAsync(string? name);

    Task<IEnumerable<Product>> FindLowStockAsync();


}