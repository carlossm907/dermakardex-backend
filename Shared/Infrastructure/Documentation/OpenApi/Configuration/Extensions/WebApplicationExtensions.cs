namespace dermakardex_backend.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;

public static class WebApplicationExtensions
{
    public static void UseOpenApiDocumentation(this WebApplication app)
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public static void UseCorsPolicy(this WebApplication app)
    {
        app.UseCors("AllowAllPolicy");
    }
}