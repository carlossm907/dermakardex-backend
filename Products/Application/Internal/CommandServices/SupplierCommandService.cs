using dermakardex_backend.Products.Domain.Model.Commands.Supplier;
using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.CommandServices;

public class SupplierCommandService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork) : ISupplierCommandService
{
    public async Task<Supplier?> Handle(CreateSupplierCommand command)
    {
        var supplier = new Supplier(command.Name);
        await supplierRepository.AddAsync(supplier);

        await unitOfWork.CompleteAsync();
        return supplier;
    }

    public async Task<Supplier?> Handle(UpdateSupplierCommand command)
    {
        var supplier = await supplierRepository.FindByIdAsync(command.SupplierId);
        if (supplier is null) return null;

        supplier.SetName(command.Name);

        await unitOfWork.CompleteAsync();
        return supplier;
    }

    public async Task<bool> Handle(DeleteSupplierCommand command)
    {
        var supplier = await supplierRepository.FindByIdAsync(command.SupplierId);
        if (supplier is null) return false;

        supplierRepository.Remove(supplier);

        await unitOfWork.CompleteAsync();

        return true;
    }
}
