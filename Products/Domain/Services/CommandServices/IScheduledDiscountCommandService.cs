using Products.Domain.Model.Commands.ScheduledDiscount;
using Products.Domain.Model.Entities;

namespace Products.Domain.Services;

public interface IScheduledDiscountCommandService
{
    Task<ProductDiscount> Handle(ScheduleDiscountToProductCommand command);

    Task Handle(ScheduleDiscountToProductsCommand command);

    Task Handle(ScheduleDiscountToAllProductsCommand command);

    Task Handle(UpdateScheduledDiscountCommand command);

    Task Handle(DeleteScheduledDiscountCommand command);

    Task Handle(CleanupExpiredProductDiscountsCommand command);

    Task Handle(DisableScheduledDiscountCommand command);
}