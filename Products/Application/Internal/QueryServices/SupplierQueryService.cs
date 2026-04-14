using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries.Supplier;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.QueryServices;

public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query)
    {
        return await supplierRepository.ListAsync();
    }

    public async Task<Supplier?> Handle(GetSupplierByIdQuery query)
    {
        return await supplierRepository.FindByIdAsync(query.SupplierId);
    }
}