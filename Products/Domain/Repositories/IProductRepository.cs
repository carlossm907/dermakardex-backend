using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Aggregates;

namespace Products.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>

{

    Task<IEnumerable<Product>> ListAsync(string? Name);

    Task<IEnumerable<Product>> FindLowStockAsync();

    Task<Product?> FindByCodeAsync(string Code);

}