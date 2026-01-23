using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using IAM.Infrastructure.Persistence.EF.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyIamConfiguration();

        builder.ApplyProductsConfiguration();

        builder.UseSnakeCaseNamingConvention();
    }
}