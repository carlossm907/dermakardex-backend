using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dermakardex_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToProductDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "product_discounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "product_discounts");
        }
    }
}
