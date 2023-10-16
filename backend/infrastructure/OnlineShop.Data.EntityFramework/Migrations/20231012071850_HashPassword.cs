using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class HashPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Accounts",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Accounts",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Accounts",
                newName: "Password");
        }
    }
}
