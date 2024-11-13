using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreServicesApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToBill1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "productName",
                table: "Bills",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productName",
                table: "Bills");
        }
    }
}
