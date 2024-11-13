using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreServicesApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bills",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Bills",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Bills",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Bills");
        }
    }
}
