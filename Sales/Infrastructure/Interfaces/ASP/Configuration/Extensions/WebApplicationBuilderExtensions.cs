using Sales.Application.ACL;
using Sales.Application.Internal.CommandServices;
using Sales.Application.Internal.QueryServices;
using Sales.Application.Internal.OutboundServices;
using Sales.Domain.Repositories;
using Sales.Domain.Services;
using Sales.Infrastructure.Persistence.EFC.Repositories;
using Sales.Infrastructure.ExternalServices.Dni;
using Sales.Interfaces.ACL;

namespace Sales.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSalesContextServices(this WebApplicationBuilder builder)
    {
        // Command Services
        builder.Services.AddScoped<ISaleCommandService, SaleCommandService>();

        // Query Services
        builder.Services.AddScoped<ISaleQueryService, SaleQueryService>();
        builder.Services.AddScoped<ISaleByClientQueryService, SaleByClientQueryService>();
        builder.Services.AddScoped<ISaleBySellerQueryService, SaleBySellerQueryService>();
        builder.Services.AddScoped<ISaleFilterQueryService, SaleFilterQueryService>();
        builder.Services.AddScoped<ISaleForStockReportQueryService, SaleForStockReportQueryService>();

        // Repositories
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();

        // Facades
        builder.Services.AddScoped<ISalesContextFacade, SalesContextFacade>();
    }
}