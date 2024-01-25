using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackGame.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8thmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Software_Data");

            migrationBuilder.RenameColumn(
                name: "uploadId",
                table: "Software_Data",
                newName: "UploadId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Software_Data",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "strength",
                table: "Software_Data",
                newName: "Strength");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "Software_Data",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "isInstalled",
                table: "Software_Data",
                newName: "IsInstalled");

            migrationBuilder.RenameColumn(
                name: "isDeleteable",
                table: "Software_Data",
                newName: "IsDeleteable");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Software_Data",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Software_Data",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadId",
                table: "Software_Data",
                newName: "uploadId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Software_Data",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Software_Data",
                newName: "strength");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Software_Data",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "IsInstalled",
                table: "Software_Data",
                newName: "isInstalled");

            migrationBuilder.RenameColumn(
                name: "IsDeleteable",
                table: "Software_Data",
                newName: "isDeleteable");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Software_Data",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Software_Data",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Software_Data",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
