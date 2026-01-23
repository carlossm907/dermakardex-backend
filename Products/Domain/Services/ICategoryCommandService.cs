using Products.Domain.Model.Commands;
using Products.Domain.Model.Entities;

namespace Products.Domain.Services;

public interface ICategoryCommandService
{
    Task<Category?> Handle(CreateCategoryCommand command);
    Task<Category?> Handle(UpdateCategoryCommand command);
    Task<bool> Handle(DeleteCategoryCommand command);
}