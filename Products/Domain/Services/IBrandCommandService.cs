using Products.Domain.Model.Commands;
using Products.Domain.Model.Entitites;

namespace Products.Domain.Services;

public interface IBrandCommandService
{
    Task<Brand?> Handle(CreateBrandCommand command);
    Task<Brand?> Handle(UpdateBrandCommand command);
    Task Handle(DeleteBrandCommand command);
}