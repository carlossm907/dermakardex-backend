using Sales.Domain.Model.ReadModels;

namespace Sales.Interfaces.ACL;

public interface ISalesContextFacade
{
    Task<IEnumerable<ProductSalesPerDay>> FetchProductSalesPerDay(
        int productId,
        DateOnly from,
        DateOnly to
    );

    Task<IEnumerable<ProductSalesPerDay>> FetchProductsSalesPerDay(
        DateOnly from,
        DateOnly to
    );
}