using IAM.Domain.Model.Aggregates;
using IAM.Domain.Model.Entities;
using IAM.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Persistence.EF.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>(user =>
        {
            user.ToTable("users");

            user.HasKey(u => u.Id);

            user.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            user.Property(u => u.FullName)
                .HasConversion(
                    fn => fn.Value,
                    value => new FullName(value))
                .HasColumnName("full_name")
                .IsRequired();

            user.Property(u => u.Username)
                .HasConversion(
                    un => un.Value,
                    value => new Username(value))
                .HasColumnName("username")
                .IsRequired();

            user.HasIndex(u => u.Username)
                .IsUnique();

            user.Property(u => u.PasswordHash)
                .HasConversion(
                    ph => ph.Value,
                    value => new PasswordHash(value))
                .HasColumnName("password_hash")
                .IsRequired();

            user.Property(u => u.Role)
                .HasConversion(
                    r => r.Name.ToString(),
                    value => Role.FromName(value))
                .HasColumnName("role")
                .IsRequired();
        });
    }
}
