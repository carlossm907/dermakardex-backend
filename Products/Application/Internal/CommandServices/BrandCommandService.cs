using dermakardex_backend.Products.Domain.Model.Commands.Brand;
using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.CommandServices;

public class BrandCommandService(IBrandRepository brandRepository, IUnitOfWork unitOfWork) : IBrandCommandService
{
    public async Task<Brand?> Handle(CreateBrandCommand command)
    {
        var brand = new Brand(command.Name);
        await brandRepository.AddAsync(brand);
        await unitOfWork.CompleteAsync();

        return brand;

    }

    public async Task<Brand?> Handle(UpdateBrandCommand command)
    {
        var brand = await brandRepository.FindByIdAsync(command.BrandId);
        if (brand is null) return null;

        brand.SetName(command.Name);
        await unitOfWork.CompleteAsync();

        return brand;
    }

    public async Task<bool> Handle(DeleteBrandCommand command)
    {
        var brand = await brandRepository.FindByIdAsync(command.BrandId);
        if (brand is null) return false;

        brandRepository.Remove(brand);
        await unitOfWork.CompleteAsync();

        return true;
    }
}