using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries;

namespace Products.Domain.Services;

public interface ICategoryQueryService
{
    Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query);
}