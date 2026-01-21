using dermakardex_backend.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using dermakardex_backend.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;
using dermakardex_backend.Shared.Infrastructure.Interfaces.ASP.Configuration;
using dermakardex_backend.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using dermakardex_backend.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

builder.AddDatabaseServices();

builder.AddOpenApiDocumentationServices();

builder.AddCorsServices();

builder.AddSharedContextServices();
builder.AddIamContextServices();

builder.AddCortexConfigurationServices();

var app = builder.Build();

app.UseDatabaseCreationAssurance();
app.UseOpenApiDocumentation();
app.UseCorsPolicy();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRequestAuthorization();
app.MapControllers();
app.Run();