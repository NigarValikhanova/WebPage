using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductTrendRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrendRate",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrendRate",
                table: "Products");
        }
    }
}
