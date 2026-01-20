using dermakardex_backend.Shared.Domain.Repositories;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace dermakardex_backend.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSharedContextServices(this WebApplicationBuilder builder)
    {
        // Profiles Bounded Context Dependency Injection Configuration
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}