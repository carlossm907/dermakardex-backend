using Products.Domain.Model.ValueObjects;
using Shared.Domain.Model.ValueObjects;

namespace Products.Domain.Model.Entities;

public class ProductDiscount
{
    public int Id { get; private set; }
    public Discount Discount { get; private set; }
    public DateTime StartsAt { get; private set; }
    public DateTime EndsAt { get; private set; }
    public bool IsActive { get; private set; }

    protected ProductDiscount() { }

    public ProductDiscount(
        Discount discount,
        DateTime startsAt,
        DateTime endsAt
    )
    {
        if (endsAt < startsAt)
        {
            throw new ArgumentException("End date must be greater than start date");
        }

        Discount = discount;
        StartsAt = startsAt;
        EndsAt = endsAt;
        IsActive = true;
    }

    public bool IsCurrentlyActive()
    {
        var now = DateTime.UtcNow;

        return IsActive && now >= StartsAt && now <= EndsAt;
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

        Discount = discount;
        StartsAt = startsAt;
        EndsAt = endsAt;
    }
}