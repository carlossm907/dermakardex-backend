using Products.Domain.Model.Commands.ProductDiscount;
using Products.Interfaces.REST.Resources;

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