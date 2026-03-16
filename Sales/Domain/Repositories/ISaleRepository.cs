using dermakardex_backend.Shared.Domain.Repositories;
using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.ReadModels;

namespace Sales.Domain.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<int> GetNextTicketSequenceAsync();
    Task<IEnumerable<Sale>> FindAllOrderedAsync();
    Task<IEnumerable<Sale>> FindByCustomerDniAsync(string customerDni);
    Task<IEnumerable<Sale>> FindBySellerUserIdAsync(int sellerUserId);
    Task<IEnumerable<Sale>> FindByProductIdAsync(int productId);
    Task<IEnumerable<Sale>> FindByDayAsync(DateOnly day);
    Task<IEnumerable<Sale>> FindByMonthAsync(int year, int month);
    Task<Sale?> FindByIdWithDetailsAsync(int saleId);
    Task<IEnumerable<Sale>> FindByDayWithDetailsAsync(DateOnly day);
    Task<IEnumerable<Sale>> FindByMonthWithDetailsAsync(int year, int month);
    Task<IEnumerable<ProductSalesPerDay>> FindProductSalesPerDayAsync(int productId, DateOnly from, DateOnly to);
    Task<IEnumerable<ProductSalesPerDay>> FindProductsSalesPerDayAsync(DateOnly from, DateOnly to);

}