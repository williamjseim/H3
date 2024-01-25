using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackGame.Api.Migrations
{
    /// <inheritdoc />
    public partial class _7thmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Software_Data_Databases_DatabaseId",
                table: "Software_Data");

            migrationBuilder.RenameColumn(
                name: "secretKey",
                table: "Hacked_Databases",
                newName: "SecretKey");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Databases",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Databases",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "storageMb",
                table: "Databases",
                newName: "StorageMb");

            migrationBuilder.RenameColumn(
                name: "secret_Verifycation_Key",
                table: "Databases",
                newName: "Secret_Verifycation_Key");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Databases",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "money",
                table: "Databases",
                newName: "Money");

            migrationBuilder.RenameColumn(
                name: "memory",
                table: "Databases",
                newName: "Memory");

            migrationBuilder.RenameColumn(
                name: "ipAddress",
                table: "Databases",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "internetKbs",
                table: "Databases",
                newName: "InternetKbs");

            migrationBuilder.RenameColumn(
                name: "indexDescription",
                table: "Databases",
                newName: "IndexDescription");

            migrationBuilder.AlterColumn<Guid>(
                name: "DatabaseId",
                table: "Software_Data",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Log",
                table: "Databases",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "VpnConnection",
                table: "Databases",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Databases_VpnConnection",
                table: "Databases",
                column: "VpnConnection");

            migrationBuilder.AddForeignKey(
                name: "FK_Databases_Databases_VpnConnection",
                table: "Databases",
                column: "VpnConnection",
                principalTable: "Databases",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Data_Databases_DatabaseId",
                table: "Software_Data",
                column: "DatabaseId",
                principalTable: "Databases",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Databases_Databases_VpnConnection",
                table: "Databases");

            migrationBuilder.DropForeignKey(
                name: "FK_Software_Data_Databases_DatabaseId",
                table: "Software_Data");

            migrationBuilder.DropIndex(
                name: "IX_Databases_VpnConnection",
                table: "Databases");

            migrationBuilder.DropColumn(
                name: "Log",
                table: "Databases");

            migrationBuilder.DropColumn(
                name: "VpnConnection",
                table: "Databases");

            migrationBuilder.RenameColumn(
                name: "SecretKey",
                table: "Hacked_Databases",
                newName: "secretKey");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Databases",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Databases",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "StorageMb",
                table: "Databases",
                newName: "storageMb");

            migrationBuilder.RenameColumn(
                name: "Secret_Verifycation_Key",
                table: "Databases",
                newName: "secret_Verifycation_Key");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Databases",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Money",
                table: "Databases",
                newName: "money");

            migrationBuilder.RenameColumn(
                name: "Memory",
                table: "Databases",
                newName: "memory");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "Databases",
                newName: "ipAddress");

            migrationBuilder.RenameColumn(
                name: "InternetKbs",
                table: "Databases",
                newName: "internetKbs");

            migrationBuilder.RenameColumn(
                name: "IndexDescription",
                table: "Databases",
                newName: "indexDescription");

            migrationBuilder.AlterColumn<Guid>(
                name: "DatabaseId",
                table: "Software_Data",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Data_Databases_DatabaseId",
                table: "Software_Data",
                column: "DatabaseId",
                principalTable: "Databases",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
