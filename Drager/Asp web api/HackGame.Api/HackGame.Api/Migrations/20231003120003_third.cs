using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackGame.Api.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hacked_Databases",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    databaseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    secretKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hacked_Databases", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hacked_Databases_Databases_databaseId",
                        column: x => x.databaseId,
                        principalTable: "Databases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hacked_Databases_Login_Data_userId",
                        column: x => x.userId,
                        principalTable: "Login_Data",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Hacked_Databases_databaseId",
                table: "Hacked_Databases",
                column: "databaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Hacked_Databases_userId",
                table: "Hacked_Databases",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hacked_Databases");
        }
    }
}
