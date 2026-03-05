using Sales.Domain.Model.Aggregates;
using Sales.Interfaces.REST.Resources;

namespace Sales.Interfaces.REST.Transform;

public class SaleDetailResourceFromEntityAssembler
{
    public static SaleDetailResource ToResourceFromEntity(Sale sale)
    {
        return new SaleDetailResource(
            sale.Id,
            sale.TicketNumber,
            sale.CustomerDni,
            sale.CustomerFullName,
            sale.SellerUserId,
            sale.SellerFullName,
            sale.SaleDate,
            sale.SaleTime,
            sale.Observation,
            sale.Total.Amount,
            sale.Status.ToString(),
            sale.Items.Select(SaleItemResourceFromEntityAssembler.ToResourceFromEntity).ToList(),
            sale.Payments.Select(SalePaymentResourceFromEntityAssembler.ToResourceFromEntity).ToList()
        );
    }
}