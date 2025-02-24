using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class AssetValueNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "AssetValues",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "AssetValues");
        }
    }
}
