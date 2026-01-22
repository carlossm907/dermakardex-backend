using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.QueryServices;

public class SupplierQueriyService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query)
    {
        return await supplierRepository.ListAsync();
    }
}