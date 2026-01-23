using Products.Domain.Model.Entitites;
using Products.Domain.Model.Queries;

namespace Products.Domain.Services;

public interface IBrandQueryService
{
    Task<IEnumerable<Brand>> Handle(GetAllBrandsQuery query);

    Task<Brand?> Handle(GetBrandByIdQuery query);
}