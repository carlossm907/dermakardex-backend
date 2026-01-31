using Sales.Domain.Model.Entities;
using Sales.Domain.Model.ValueObjects;
using Shared.Domain.Model.ValueObjects;

namespace Sales.Domain.Model.Aggregates;

public class Sale
{
    public int Id { get; private set; }
    public SaleDocumentType DocumentType { get; private set; }
    public string TicketNumber { get; private set; }
    public DateOnly SaleDate { get; private set; }
    public TimeOnly SaleTime { get; private set; }
    public string CustomerDni { get; private set; }
    public string CustomerFullName { get; private set; }
    public int SellerUserId { get; private set; }
    public string SellerFullName { get; private set; }
    public string? Observation { get; private set; }
    public Money Total { get; private set; }
    public SaleStatus Status { get; private set; }
    private readonly List<SaleItem> _items = new();
    private readonly List<SalePayment> _payments = new();

    protected Sale()
    {
        TicketNumber = string.Empty;
        CustomerDni = string.Empty;
        CustomerFullName = string.Empty;
        SellerFullName = string.Empty;

        Total = new Money(0);
        Status = SaleStatus.COMPLETED;
        DocumentType = SaleDocumentType.TICKET;
    }

    public Sale(
        string ticketNumber,
        DateOnly saleDate,
        TimeOnly saleTime,
        string customerDni,
        string customerFullName,
        int sellerUserId,
        string sellerFullName,
        string? observation = null
    )
    {
        TicketNumber = ticketNumber.Trim();
        DocumentType = SaleDocumentType.TICKET;

        SaleDate = saleDate;
        SaleTime = saleTime;

        CustomerDni = customerDni.Trim();
        CustomerFullName = customerFullName.Trim();

        SellerUserId = sellerUserId;
        SellerFullName = sellerFullName.Trim();

        Observation = string.IsNullOrWhiteSpace(observation) ? null : observation.Trim();

        Total = new Money(0);
        Status = SaleStatus.COMPLETED;
    }

    public void AddItem(SaleItem item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        _items.Add(item);
        RecalculateTotal();
    }

    public void AddPayment(SalePayment payment)
    {
        if (payment is null)
        {
            throw new ArgumentNullException(nameof(payment));
        }

        _payments.Add(payment);
    }

    public void FinalizeSale()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Sale must have at least one item.");

        RecalculateTotal();
        ValidatePayments();

        Status = SaleStatus.COMPLETED;
    }

    private void RecalculateTotal()
    {
        Money total = new Money(0);
        foreach (var item in _items)
            total = total.Add(item.LineTotal);

        Total = total;
    }

    private void ValidatePayments()
    {
        Money sum = new Money(0);
        foreach (var payment in _payments)
            sum = sum.Add(payment.Amount);

        if (Math.Abs(sum.Amount - Total.Amount) > 0.01m)
            throw new InvalidOperationException("Payments must match total amount.");
    }
}