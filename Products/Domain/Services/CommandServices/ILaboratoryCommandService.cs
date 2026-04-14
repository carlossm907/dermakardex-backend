using dermakardex_backend.Products.Domain.Model.Commands.Laboratory;
using Products.Domain.Model.Entities;

namespace Products.Domain.Services;

public interface ILaboratoryCommandService
{
    Task<Laboratory?> Handle(CreateLaboratoryCommand command);
    Task<Laboratory?> Handle(UpdateLaboratoryCommand command);
    Task<bool> Handle(DeleteLaboratoryCommand command);
}