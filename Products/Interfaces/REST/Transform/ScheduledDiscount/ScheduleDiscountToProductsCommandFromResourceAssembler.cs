using Products.Domain.Model.Commands.ScheduledDiscount;
using Products.Interfaces.REST.Resources.ScheduledDiscount;

namespace Products.Interfaces.REST.Transform;

public static class ScheduleDiscountToProductsCommandFromResourceAssembler
{
    public static ScheduleDiscountToProductsCommand ToCommandFromResource(
        ScheduleDiscountToProductsResource resource)
    {
        return new ScheduleDiscountToProductsCommand(
            resource.ProductIds,
            resource.Name,
            resource.DiscountType,
            resource.DiscountValue,
            resource.StartsAt,
            resource.EndsAt
        );
    }
}