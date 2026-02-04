using Sales.Application.Internal.CommandServices;
using Sales.Application.Internal.QueryServices;
using Sales.Domain.Repositories;
using Sales.Domain.Services;
using Sales.Infrastructure.Persistence.EFC.Repositories;

namespace Sales.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSalesContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISaleCommandService, SaleCommandService>();
        builder.Services.AddScoped<ISaleQueryService, SaleQueryService>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
    }
}