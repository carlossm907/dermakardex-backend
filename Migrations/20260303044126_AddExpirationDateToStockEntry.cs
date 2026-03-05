using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dermakardex_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddExpirationDateToStockEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "expiration_date",
                table: "product_stock_entries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expiration_date",
                table: "product_stock_entries");
        }
    }
}
