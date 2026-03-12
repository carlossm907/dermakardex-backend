using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.Model.Aggregates;
using Sales.Domain.Repositories;

namespace Sales.Infrastructure.Persistence.EFC.Repositories;

public class SaleRepository(AppDbContext context) : BaseRepository<Sale>(context), ISaleRepository
{
    public async Task<IEnumerable<Sale>> FindAllOrderedAsync()
    {
        return await Context.Set<Sale>()
            .OrderByDescending(s => s.SaleDate)
            .ThenByDescending(s => s.SaleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> FindByCustomerDniAsync(string customerDni)
    {
        return await Context.Set<Sale>()
            .Where(s => s.CustomerDni == customerDni)
            .OrderByDescending(s => s.SaleDate)
            .ThenByDescending(s => s.SaleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> FindByDayAsync(DateOnly day)
    {
        return await Context.Set<Sale>()
            .Where(s => s.SaleDate == day)
            .OrderByDescending(s => s.SaleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> FindByDayWithDetailsAsync(DateOnly day)
    {
        return await Context.Set<Sale>()
        .Include(s => s.Items)
        .Include(s => s.Payments)
        .Where(s => s.SaleDate == day)
        .OrderBy(s => s.CustomerFullName)
        .ThenBy(s => s.SaleTime)
        .ToListAsync();
    }

    public async Task<Sale?> FindByIdWithDetailsAsync(int saleId)
    {
        return await Context.Set<Sale>()
        .Include(s => s.Items)
        .Include(s => s.Payments)
        .FirstOrDefaultAsync(s => s.Id == saleId);
    }

    public async Task<IEnumerable<Sale>> FindByMonthAsync(int year, int month)
    {
        return await Context.Set<Sale>()
            .Where(s => s.SaleDate.Year == year && s.SaleDate.Month == month)
            .OrderByDescending(s => s.SaleDate)
            .ThenByDescending(s => s.SaleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> FindByMonthWithDetailsAsync(int year, int month)
    {
        return await Context.Set<Sale>()
        .Include(s => s.Items)
        .Include(s => s.Payments)
        .Where(s => s.SaleDate.Year == year && s.SaleDate.Month == month)
        .OrderBy(s => s.CustomerFullName)
        .ThenBy(s => s.SaleDate)
        .ThenBy(s => s.SaleTime)
        .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> FindByProductIdAsync(int productId)
    {
        return await Context.Set<Sale>()
            .Where(s => s.Items.Any(i => i.ProductId == productId))
            .OrderByDescending(s => s.SaleDate)
            .ThenByDescending(s => s.SaleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> FindBySellerUserIdAsync(int sellerUserId)
    {
        return await Context.Set<Sale>()
            .Where(s => s.SellerUserId == sellerUserId)
            .OrderByDescending(s => s.SaleDate)
            .ThenByDescending(s => s.SaleTime)
            .ToListAsync();
    }

    public async Task<int> GetNextTicketSequenceAsync()
    {
        var lastTicket = await Context.Set<Sale>()
       .OrderByDescending(s => s.Id)
       .Select(s => s.TicketNumber)
       .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(lastTicket))
            return 1;

        var numericPart = lastTicket.Substring(1);

        return int.TryParse(numericPart, out var last)
            ? last + 1
            : 1;
    }


}