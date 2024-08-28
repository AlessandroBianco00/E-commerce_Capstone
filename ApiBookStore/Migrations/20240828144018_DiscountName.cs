using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBookStore.Migrations
{
    /// <inheritdoc />
    public partial class DiscountName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscountName",
                table: "Discounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountName",
                table: "Discounts");
        }
    }
}
