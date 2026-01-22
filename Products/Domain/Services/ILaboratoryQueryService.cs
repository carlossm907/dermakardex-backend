using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries;

namespace Products.Domain.Services;

public interface ILaboratoryQueryService
{
    Task<IEnumerable<Laboratory>> Handle(GetAllLaboratoriesQuery query);
}