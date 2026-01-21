using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using IAM.Infrastructure.Persistence.EF.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyIamConfiguration();
        
        // Use snake case for database objects and pluralization for table names
        builder.UseSnakeCaseNamingConvention();
    }
}