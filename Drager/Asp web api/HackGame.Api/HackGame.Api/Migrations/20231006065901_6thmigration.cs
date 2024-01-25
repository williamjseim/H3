using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackGame.Api.Migrations
{
    /// <inheritdoc />
    public partial class _6thmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StaticObject",
                table: "Software_Data",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaticObject",
                table: "Software_Data");
        }
    }
}
