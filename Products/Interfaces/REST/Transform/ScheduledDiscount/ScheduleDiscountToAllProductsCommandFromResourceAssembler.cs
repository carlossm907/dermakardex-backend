using Products.Domain.Model.Commands.ScheduledDiscount;
using Products.Interfaces.REST.Resources.ScheduledDiscount;

namespace Products.Interfaces.REST.Transform;

public static class ScheduleDiscountToAllProductsCommandFromResourceAssembler
{
    public static ScheduleDiscountToAllProductsCommand ToCommandFromResource(
        ScheduleDiscountToAllProductsResource resource)
    {
        return new ScheduleDiscountToAllProductsCommand(
            resource.Name,
            resource.DiscountType,
            resource.DiscountValue,
            resource.StartsAt,
            resource.EndsAt
        );
    }
}