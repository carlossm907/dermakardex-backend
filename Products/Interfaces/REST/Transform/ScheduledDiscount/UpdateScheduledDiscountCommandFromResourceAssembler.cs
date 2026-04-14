using Products.Domain.Model.Commands.ScheduledDiscount;
using Products.Interfaces.REST.Resources.ScheduledDiscount;

namespace Products.Interfaces.REST.Transform;

public static class UpdateScheduledDiscountCommandFromResourceAssembler
{
    public static UpdateScheduledDiscountCommand ToCommandFromResource(int discountId, UpdateScheduledDiscountResource resource)
    {
        return new UpdateScheduledDiscountCommand(
            discountId,
            resource.Name,
            resource.Type,
            resource.Value,
            resource.StartsAt,
            resource.EndsAt
        );
    }
}