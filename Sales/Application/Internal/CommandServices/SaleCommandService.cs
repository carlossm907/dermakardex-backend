using dermakardex_backend.Shared.Domain.Repositories;
using IAM.Interfaces.ACL;
using Products.Interfaces.ACL;
using Sales.Application.Internal.OutboundServices;
using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Commands;
using Sales.Domain.Model.Entities;
using Sales.Domain.Repositories;
using Sales.Domain.Services;
using Shared.Domain.Model.ValueObjects;

namespace Sales.Application.Internal.CommandServices;

public class SaleCommandService : ISaleCommandService
{

    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIamContextFacade _iamContextFacade;
    private readonly IDniLookupService _dniLookupService;
    private readonly IProductsContextFacade _productsContextFacade;

    public SaleCommandService(
        ISaleRepository saleRepository,
        IUnitOfWork unitOfWork,
        IIamContextFacade iamContextFacade,
        IDniLookupService dniLookupService,
        IProductsContextFacade productsContextFacade
    )
    {
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
        _iamContextFacade = iamContextFacade;
        _dniLookupService = dniLookupService;
        _productsContextFacade = productsContextFacade;
    }

    public async Task<Sale> Handle(RegisterSaleCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.CustomerDni))
        {
            throw new ArgumentException("Custumer DNI is required");
        }

        if (command.Items is null || command.Items.Count == 0)
        {
            throw new ArgumentException("Sale must have at least one item");
        }

        if (command.Payments is null || command.Payments.Count == 0)
        {
            throw new ArgumentException("Sale must have at least one payment");
        }

        var sellerUserId = _iamContextFacade.GetCurrentUserId();
        var sellerFullName = _iamContextFacade.GetCurrentUserFullName();

        var customerDni = command.CustomerDni.Trim();

        var customerFullName = await _dniLookupService.GetFullNameByDniAsync(customerDni);

        if (string.IsNullOrWhiteSpace(customerFullName))
        {
            throw new InvalidOperationException($"Customer full name could not be resolved for DNI {customerDni}.");
        }

        var now = DateTime.Now;
        var ticketNumber = await GenerateTicketNumberAsync();

        var sale = new Sale(
            ticketNumber,
            DateOnly.FromDateTime(now),
            TimeOnly.FromDateTime(now),
            customerDni,
            customerFullName,
            sellerUserId,
            sellerFullName,
            command.Observation
        );

        foreach (var itemCommand in command.Items)
        {
            if (itemCommand.Quantity <= 0)
            {
                throw new ArgumentException("Item quantity must be greater than zero");
            }

            var product = await _productsContextFacade.GetProductForSaleAsync(itemCommand.ProductId);

            if (product.AvailableStock < itemCommand.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock for product {product.Name}.");
            }

            var saleItem = new SaleItem(
                product.ProductId,
                product.Name,
                (ProductPresentation)product.PresentationUnits,
                itemCommand.Quantity,
                new Money(product.BaseUnitPrice),
                new Money(product.FinalUnitPrice),
                product.DiscountType,
                product.DiscountValue
            );

            sale.AddItem(saleItem);
        }

        foreach (var paymentCommand in command.Payments)
        {
            var payment = new SalePayment(
                paymentCommand.Method,
                new Money(paymentCommand.Amount)
            );

            sale.AddPayment(payment);
        }

        sale.FinalizeSale();

        foreach (var item in command.Items)
        {
            await _productsContextFacade.ReduceStockAsync(item.ProductId, item.Quantity);
        }

        await _saleRepository.AddAsync(sale);
        await _unitOfWork.CompleteAsync();

        return sale;
    }

    private async Task<string> GenerateTicketNumberAsync()
    {
        var next = await _saleRepository.GetNextTicketSequenceAsync();
        return $"T{next:D5}";
    }
}

