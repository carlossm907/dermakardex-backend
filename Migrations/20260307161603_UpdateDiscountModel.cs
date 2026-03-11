using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dermakardex_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiscountModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "i_x_products_code",
                table: "products",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "i_x_products_code",
                table: "products");
        }
    }
}
