using Sales.Domain.Model.Aggregates;
using Sales.Interfaces.REST.Resources;

namespace Sales.Interfaces.REST.Transform;

public static class SaleResourceFromEntityAssembler
{
    public static SaleResource ToResourceFromEntity(Sale sale)
    {
        return new SaleResource(
            sale.Id,
            sale.TicketNumber,
            sale.CustomerFullName,
            sale.SellerFullName,
            sale.SaleDate,
            sale.SaleTime,
            sale.Total.Amount,
            sale.Status.ToString()
        );
    }
}