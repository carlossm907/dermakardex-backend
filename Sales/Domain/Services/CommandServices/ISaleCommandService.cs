using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Commands;

namespace Sales.Domain.Services;

public interface ISaleCommandService
{
    Task<Sale> Handle(RegisterSaleCommand command);
}