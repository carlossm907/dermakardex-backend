using Products.Interfaces.ACL.Dto;

namespace Products.Interfaces.ACL;

public interface IProductsContextFacade
{
    Task<ProductForSaleDto> GetProductForSaleAsync(int productId);
    Task ReduceStockAsync(int productId, int quantity);
}