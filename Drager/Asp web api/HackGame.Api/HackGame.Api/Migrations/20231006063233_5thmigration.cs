using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackGame.Api.Migrations
{
    /// <inheritdoc />
    public partial class _5thmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EncryptionStrength",
                table: "Software_Data",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HiddenStrength",
                table: "Software_Data",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptionStrength",
                table: "Software_Data");

            migrationBuilder.DropColumn(
                name: "HiddenStrength",
                table: "Software_Data");
        }
    }
}
