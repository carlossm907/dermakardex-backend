using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Commands;
using Products.Domain.Model.Entities;
using Products.Domain.Repositoies;
using Products.DomainServices;

namespace Products.Application.Internal.CommandServices;

public class CategoryCommandService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICategoryCommandService
{
    public async Task<Category?> Handle(CreateCategoryCommand command)
    {
        var category = new Category(command.Name);
        await categoryRepository.AddAsync(category);
        await unitOfWork.CompleteAsync();

        return category;
    }

    public async Task<Category?> Handle(UpdateCategoryCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.CategoryId);
        if (category is null) return null;

        category.SetName(command.Name);
        await unitOfWork.CompleteAsync();

        return category;

    }

    public async Task<bool> Handle(DeleteCategoryCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.CategoryId);
        if (category is null) return false;

        categoryRepository.Remove(category);
        await unitOfWork.CompleteAsync();

        return true;
    }
}