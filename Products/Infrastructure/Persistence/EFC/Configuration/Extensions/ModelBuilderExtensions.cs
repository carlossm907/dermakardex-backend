using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Entities;

namespace Products.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProductsConfiguration(this ModelBuilder builder)
    {
        // Product Aggregate
        builder.Entity<Product>(product =>
        {
            product.ToTable("products");

            product.HasKey(p => p.Id);

            product.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            product.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(200);

            product.HasIndex(p => p.Code)
                .IsUnique();

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

            product.Property(p => p.BrandId)
                .IsRequired();

            product.Property(p => p.CategoryId)
                .IsRequired();

            product.Property(p => p.SupplierId)
                .IsRequired();

            product.Property(p => p.LaboratoryId)
                .IsRequired();

            // Value Object: PurchasePrice
            product.OwnsOne(p => p.PurchasePrice, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("purchase_price_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("purchase_price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // Value Object: SalePrice
            product.OwnsOne(p => p.SalePrice, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("sale_price_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("sale_price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // Value Object: MaxDiscountAmount
            product.OwnsOne(p => p.MaxDiscountAmount, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("max_discount_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("max_discount_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // Value Object: Discount
            product.OwnsOne(p => p.Discount, discount =>
            {
                discount.WithOwner().HasForeignKey("Id");

                discount.Property(d => d.Type)
                    .HasColumnName("discount_type")
                    .IsRequired();

                discount.Property(d => d.Value)
                    .HasColumnName("discount_value")
                    .IsRequired();
            });

            product.Navigation(p => p.Discount)
            .IsRequired();
        });

        // Brand
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

        // Category
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

        // Laboratory
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

        // Supplier
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

        // StockEntry 
        builder.Entity<StockEntry>(entry =>
        {
            entry.ToTable("product_stock_entries");

            entry.HasKey(e => e.Id);

            entry.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entry.Property(e => e.ProductId)
                .IsRequired();

            entry.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            entry.Property(e => e.Quantity)
                .IsRequired();

            entry.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .IsRequired();

            entry.Property(e => e.Reason)
                .IsRequired()
                .HasMaxLength(300);

            entry.Property(e => e.UserFullName)
                .IsRequired()
                .HasMaxLength(200);

            entry.Property(e => e.RegisteredAt)
                .IsRequired()
                .HasDefaultValueSql("now()");


            // Value Object: UnitPurchasePrice

            entry.OwnsOne(e => e.UnitPurchasePrice, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("unit_purchase_price_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("unit_purchase_price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // Value Object: TotalInvestment
            entry.OwnsOne(e => e.TotalInvestment, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("total_investment_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("total_investment_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // FK a Product
            entry.HasOne<Product>()
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
