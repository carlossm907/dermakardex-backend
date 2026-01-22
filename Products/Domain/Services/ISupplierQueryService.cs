using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries;

namespace Products.Domain.Services;

public interface ISupplierQueryService
{
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query);
}