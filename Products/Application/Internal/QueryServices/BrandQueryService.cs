using Products.Domain.Model.Entitites;
using Products.Domain.Model.Queries;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.QueryServices;

public class BrandQueryService(IBrandRepository brandRepository) : IBrandQueryService
{
    public async Task<IEnumerable<Brand>> Handle(GetAllBrandsQuery query)
    {
        return await brandRepository.ListAsync();
    }
}