using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Accounts",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
