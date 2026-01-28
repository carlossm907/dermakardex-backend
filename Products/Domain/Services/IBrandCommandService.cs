using Products.Domain.Model.Commands;
using Products.Domain.Model.Entities;

namespace Products.Domain.Services;

public interface IBrandCommandService
{
    Task<Brand?> Handle(CreateBrandCommand command);
    Task<Brand?> Handle(UpdateBrandCommand command);
    Task<bool> Handle(DeleteBrandCommand command);
}