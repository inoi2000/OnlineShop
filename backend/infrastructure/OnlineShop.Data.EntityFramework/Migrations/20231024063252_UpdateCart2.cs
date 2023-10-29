using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CartItems",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
