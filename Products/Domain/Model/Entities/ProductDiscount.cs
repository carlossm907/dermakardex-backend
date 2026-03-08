using Products.Domain.Model.ValueObjects;
using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Entities;

public class ProductDiscount
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public Discount Discount { get; private set; }
    public DateTime StartsAt { get; private set; }
    public DateTime EndsAt { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected ProductDiscount() { }

    public ProductDiscount(
        int productId,
        Discount discount,
        DateTime startsAt,
        DateTime endsAt
    )
    {
        if (productId <= 0)
        {
            throw new ArgumentException("ProductId must be valid");
        }

        if (endsAt <= startsAt)
        {
            throw new ArgumentException("End date must be greater than start date");
        }

        ProductId = productId;
        Discount = discount ?? throw new ArgumentNullException(nameof(discount));
        StartsAt = startsAt;
        EndsAt = endsAt;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public bool IsCurrentlyActive()
    {
        var now = DateTime.UtcNow;

        return IsActive && now >= StartsAt && now < EndsAt;
    }

    public void Disable()
    {
        IsActive = false;
    }

    public void Enable()
    {
        IsActive = true;
    }

    public void Update(Discount discount, DateTime startsAt, DateTime endsAt)
    {
        if (endsAt <= startsAt)
        {
            throw new ArgumentException("End date must be greater than start date");
        }

        Discount = discount ?? throw new ArgumentNullException(nameof(discount));
        StartsAt = startsAt;
        EndsAt = endsAt;
    }
}