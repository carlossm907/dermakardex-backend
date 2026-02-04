using Microsoft.EntityFrameworkCore;
using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Entities;

namespace Sales.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySalesConfiguration(this ModelBuilder builder)
    {
        // =========================
        // Sale (Aggregate Root)
        // =========================
        builder.Entity<Sale>(sale =>
        {
            sale.ToTable("sales");

            sale.HasKey(s => s.Id);

            sale.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            sale.Property(s => s.TicketNumber)
                .IsRequired()
                .HasMaxLength(20);

            sale.Property(s => s.CustomerDni)
                .IsRequired()
                .HasMaxLength(8);

            sale.Property(s => s.CustomerFullName)
                .IsRequired()
                .HasMaxLength(200);

            sale.Property(s => s.SellerUserId)
                .IsRequired();

            sale.Property(s => s.SellerFullName)
                .IsRequired()
                .HasMaxLength(200);

            sale.Property(s => s.Observation)
                .HasMaxLength(500);

            sale.Property(s => s.Status)
                .IsRequired();

            sale.Property(s => s.DocumentType)
                .IsRequired();

            // DateOnly
            sale.Property(s => s.SaleDate)
                .HasColumnType("date")
                .IsRequired();

            // TimeOnly
            sale.Property(s => s.SaleTime)
                .HasColumnType("time")
                .IsRequired();

            // Value Object: Total (Money)
            sale.OwnsOne(s => s.Total, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("total_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("total_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // Items (map property, access field)
            sale.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey("SaleId")
                .OnDelete(DeleteBehavior.Cascade);

            sale.Navigation(s => s.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // Payments (map property, access field)
            sale.HasMany(s => s.Payments)
                .WithOne()
                .HasForeignKey("SaleId")
                .OnDelete(DeleteBehavior.Cascade);

            sale.Navigation(s => s.Payments)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        // =========================
        // SaleItem (Entity)
        // =========================
        builder.Entity<SaleItem>(item =>
        {
            item.ToTable("sale_items");

            item.HasKey(i => i.Id);

            item.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            item.Property(i => i.ProductId)
                .IsRequired();

            item.Property(i => i.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            item.Property(i => i.Presentation)
                .IsRequired();

            item.Property(i => i.Quantity)
                .IsRequired();

            item.Property(i => i.DiscountType)
                .IsRequired();

            item.Property(i => i.DiscountValue)
                .IsRequired();

            // Value Object: BaseUnitPrice
            item.OwnsOne(i => i.BaseUnitPrice, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("base_unit_price_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("base_unit_price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // Value Object: UnitPrice
            item.OwnsOne(i => i.UnitPrice, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("unit_price_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("unit_price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // Value Object: LineTotal
            item.OwnsOne(i => i.LineTotal, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("line_total_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("line_total_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        });

        // =========================
        // SalePayment (Entity)
        // =========================
        builder.Entity<SalePayment>(payment =>
        {
            payment.ToTable("sale_payments");

            payment.HasKey(p => p.Id);

            payment.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            payment.Property(p => p.Method)
                .IsRequired();

            // Value Object: Amount (Money)
            payment.OwnsOne(p => p.Amount, money =>
            {
                money.WithOwner().HasForeignKey("Id");

                money.Property(m => m.Amount)
                    .HasColumnName("payment_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("payment_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        });
    }
}
