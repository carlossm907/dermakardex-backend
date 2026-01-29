using dermakardex_backend.Products.Domain.Model.Commands.Laboratory;
using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.CommandServices;

public class LaboratoryCommandService(ILaboratoryRepository laboratoryRepository, IUnitOfWork unitOfWork) : ILaboratoryCommandService
{
    public async Task<Laboratory?> Handle(CreateLaboratoryCommand command)
    {
        var laboratory = new Laboratory(command.Name);
        await laboratoryRepository.AddAsync(laboratory);

        await unitOfWork.CompleteAsync();
        return laboratory;
    }

    public async Task<Laboratory?> Handle(UpdateLaboratoryCommand command)
    {
        var laboratory = await laboratoryRepository.FindByIdAsync(command.LaboratoryId);
        if (laboratory is null) return null;

        laboratory.SetName(command.Name);

        await unitOfWork.CompleteAsync();
        return laboratory;
    }

    public async Task<bool> Handle(DeleteLaboratoryCommand command)
    {
        var laboratory = await laboratoryRepository.FindByIdAsync(command.LaboratoryId);
        if (laboratory is null) return false;

        laboratoryRepository.Remove(laboratory);

        await unitOfWork.CompleteAsync();

        return true;

    }
}