namespace Products.Domain.Model.Commands.ProductDiscount;

public record DisableScheduledDiscountCommand(int ProductId, int ProductDiscountId);