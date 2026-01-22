using Products.Domain.Model.Commands;
using Products.Domain.Model.Entities;

namespace Products.DomainServices;

public interface ICategoryCommandService
{
    Task<Category?> Handle(CreateCategoryCommand command);
    Task<Category?> Handle(UpdateCategoryCommand command);
    Task Handle(DeleteCategoryCommand command);
}