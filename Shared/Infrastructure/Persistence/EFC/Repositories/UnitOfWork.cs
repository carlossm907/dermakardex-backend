using dermakardex_backend.Shared.Domain.Repositories;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Implementation of the unit of work pattern using Entity Framework Core.
/// </summary>
/// <param name="context">The database context.</param>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    /// <summary>
    ///     Asynchronously saves all changes made in this unit of work.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}