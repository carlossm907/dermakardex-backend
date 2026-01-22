using Products.Domain.Model.Commands;
using Products.Domain.Model.Entities;

namespace Products.Domain.Services;

public interface ILaboratoryCommandService
{
    Task<Laboratory?> Handle(CreateLaboratoryCommand command);
    Task<Laboratory?> Handle(UpdateLaboratoryCommand command);
    Task Handle(DeleteLaboratoryCommand command);
}