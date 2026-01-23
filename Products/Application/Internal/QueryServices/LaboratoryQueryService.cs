using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.QueryServices;

public class LaboratoryQueryService(ILaboratoryRepository laboratoryRepository) : ILaboratoryQueryService
{
    public async Task<IEnumerable<Laboratory>> Handle(GetAllLaboratoriesQuery query)
    {
        return await laboratoryRepository.ListAsync();
    }

    public async Task<Laboratory?> Handle(GetLaboratoryByIdQuery query)
    {
        return await laboratoryRepository.FindByIdAsync(query.LaboratoryId);
    }
}