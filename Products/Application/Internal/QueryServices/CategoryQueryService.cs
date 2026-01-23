using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries;
using Products.Domain.Repositoies;
using Products.Domain.Services;

namespace Products.Application.Internal.QueryServices;

public class CayegoryQueryService(ICategoryRepository categoryRepository) : ICategoryQueryService
{
    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query)
    {
        return await categoryRepository.ListAsync();
    }

    public async Task<Category?> Handle(GetCategoryByIdQuery query)
    {
        return await categoryRepository.FindByIdAsync(query.CategoryId);
    }
}