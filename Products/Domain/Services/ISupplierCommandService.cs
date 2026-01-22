using Products.Domain.Model.Commands;
using Products.Domain.Model.Entities;

namespace Products.Domain.Services;

public interface ISupplierCommandService
{
    Task<Supplier?> Handle(CreateSupplierCommand command);
    Task<Supplier?> Handle(UpdateSupplierCommand command);
    Task<bool> Handle(DeleteSupplierCommand command);
}