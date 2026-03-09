using Products.Domain.Model.Commands.ProductDiscount;
using Products.Interfaces.REST.Resources;
using Shared.Domain.Model.ValueObjects;

namespace Products.Interfaces.REST.Transform;

public static class ScheduleDiscountCommandFromResourceAssembler
{
    public static ScheduleDiscountToProductCommand ToCommandFromResource(ScheduleDiscountResource resource)
    {
        var type = Enum.Parse<DiscountType>(resource.Type);
        return new ScheduleDiscountToProductCommand(
            resource.ProductId,
            type,
            resource.Value,
            resource.StartsAt,
            resource.EndsAt
        );
    }
}