using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.Model.Aggregates;
using Sales.Domain.Repositories;

namespace Sales.Infrastructure.Persistence.EFC.Repositories;

public class SaleRepository(AppDbContext context) : BaseRepository<Sale>(context), ISaleRepository
{
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