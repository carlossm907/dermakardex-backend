using dermakardex_backend.Shared.Domain.Repositories;
using Sales.Domain.Model.Aggregates;

namespace Sales.Domain.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<int> GetNextTicketSequenceAsync();
}