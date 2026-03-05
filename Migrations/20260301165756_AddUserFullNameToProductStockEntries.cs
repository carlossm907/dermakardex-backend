using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dermakardex_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFullNameToProductStockEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "registered_by_user_id",
                table: "product_stock_entries");

            migrationBuilder.AlterColumn<DateTime>(
                name: "registered_at",
                table: "product_stock_entries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "user_full_name",
                table: "product_stock_entries",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_full_name",
                table: "product_stock_entries");

            migrationBuilder.AlterColumn<DateTime>(
                name: "registered_at",
                table: "product_stock_entries",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<int>(
                name: "registered_by_user_id",
                table: "product_stock_entries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
