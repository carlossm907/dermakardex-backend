using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Entities;
using Products.Domain.Model.Entitites;

namespace Products.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProductsConfiguration(this ModelBuilder builder)
    {

        builder.Entity<Product>(product =>
        {
            product.ToTable("products");

            product.HasKey(p => p.Id);

            product.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            product.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            product.Property(p => p.Presentation)
                .IsRequired();

            product.Property(p => p.IsActive)
                .IsRequired();

            product.Property(p => p.Stock)
                .IsRequired();

            product.Property(p => p.StockAlertThreshold)
                .IsRequired();

            // Value Object: Money
            product.OwnsOne(p => p.PurchasePrice, money =>
            {
                money.Property(m => m.Amount)
                     .HasColumnName("purchase_price")
                     .IsRequired();
            });

            product.OwnsOne(p => p.SalePrice, money =>
            {
                money.Property(m => m.Amount)
                     .HasColumnName("sale_price")
                     .IsRequired();
            });

            product.OwnsOne(p => p.MaxDiscountAmount, money =>
            {
                money.Property(m => m.Amount)
                     .HasColumnName("max_discount_amount")
                     .IsRequired();
            });

            product.Property(p => p.BrandId).IsRequired();
            product.Property(p => p.CategoryId).IsRequired();
            product.Property(p => p.SupplierId).IsRequired();
            product.Property(p => p.LaboratoryId).IsRequired();
        });

        builder.Entity<Brand>(brand =>
        {
            brand.ToTable("brands");

            brand.HasKey(b => b.Id);

            brand.Property(b => b.Id)
                 .ValueGeneratedOnAdd();

            brand.Property(b => b.Name)
                 .IsRequired()
                 .HasMaxLength(150);
        });

        builder.Entity<Category>(category =>
        {
            category.ToTable("categories");

            category.HasKey(c => c.Id);

            category.Property(c => c.Id)
                    .ValueGeneratedOnAdd();

            category.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(150);
        });

        builder.Entity<Laboratory>(laboratory =>
        {
            laboratory.ToTable("laboratories");

            laboratory.HasKey(l => l.Id);

            laboratory.Property(l => l.Id)
                      .ValueGeneratedOnAdd();

            laboratory.Property(l => l.Name)
                      .IsRequired()
                      .HasMaxLength(150);
        });

        builder.Entity<Supplier>(supplier =>
        {
            supplier.ToTable("suppliers");

            supplier.HasKey(s => s.Id);

            supplier.Property(s => s.Id)
                    .ValueGeneratedOnAdd();

            supplier.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(150);
        });
    }
}