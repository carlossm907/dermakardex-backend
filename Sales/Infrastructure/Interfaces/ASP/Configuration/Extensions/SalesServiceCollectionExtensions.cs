using Sales.Application.Internal.OutboundServices;
using Sales.Infrastructure.ExternalServices.Dni;

namespace Sales.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class SalesServiceCollectionExtensions
{
    public static IServiceCollection AddSalesExternalServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.Configure<ApiPeruOptions>(
            configuration.GetSection("ApiPeru"));


        services.AddHttpClient<IDniLookupService, ApiPeruDniLookupService>(
            (sp, client) =>
            {
                var options = sp
                    .GetRequiredService<
                        Microsoft.Extensions.Options.IOptions<ApiPeruOptions>>()
                    .Value;

                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(10);
            });

        return services;
    }
}
