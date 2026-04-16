using Products.Domain.Model.Commands.ScheduledDiscount;
using Products.Interfaces.REST.Resources.ScheduledDiscount;

namespace Products.Interfaces.REST.Transform;

public static class ScheduleDiscountCommandFromResourceAssembler
{
    public static ScheduleDiscountToProductCommand ToCommandFromResource(int productId, ScheduleDiscountResource resource)
    {
        return new ScheduleDiscountToProductCommand(
            productId,
            resource.Name,
            resource.Type,
            resource.Value,
            resource.StartsAt,
            resource.EndsAt
        );
    }
}