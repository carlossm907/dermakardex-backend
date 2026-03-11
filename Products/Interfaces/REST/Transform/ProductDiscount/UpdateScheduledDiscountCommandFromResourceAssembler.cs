using Products.Domain.Model.Commands.ProductDiscount;
using Products.Interfaces.REST.Resources;
using Shared.Domain.Model.ValueObjects;

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