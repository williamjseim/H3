using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackGame.Api.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Login_Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login_Data", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Databases",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserIdFk = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ipAddress = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: false),
                    ProcessorGhz = table.Column<float>(type: "float", nullable: false),
                    storageMb = table.Column<int>(type: "int", nullable: false),
                    memory = table.Column<int>(type: "int", nullable: false),
                    internetKbs = table.Column<int>(type: "int", nullable: false),
                    money = table.Column<int>(type: "int", nullable: false),
                    secret_Verifycation_Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Databases", x => x.id);
                    table.ForeignKey(
                        name: "FK_Databases_Login_Data_UserIdFk",
                        column: x => x.UserIdFk,
                        principalTable: "Login_Data",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inventory_Data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    highTechComps = table.Column<int>(type: "int", nullable: false),
                    techComps = table.Column<int>(type: "int", nullable: false),
                    microControllers = table.Column<int>(type: "int", nullable: false),
                    MilitaryTechComps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory_Data", x => x.id);
                    table.ForeignKey(
                        name: "FK_Inventory_Data_Login_Data_userId",
                        column: x => x.userId,
                        principalTable: "Login_Data",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Software_Data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type = table.Column<int>(type: "int", nullable: false),
                    isInstalled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    strength = table.Column<float>(type: "float", nullable: false),
                    size = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isDeleteable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DatabaseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software_Data", x => x.id);
                    table.ForeignKey(
                        name: "FK_Software_Data_Databases_DatabaseId",
                        column: x => x.DatabaseId,
                        principalTable: "Databases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Timed_Tasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    endTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    softwareId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type = table.Column<int>(type: "int", nullable: false),
                    targetIp = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timed_Tasks", x => x.id);
                    table.ForeignKey(
                        name: "FK_Timed_Tasks_Login_Data_userId",
                        column: x => x.userId,
                        principalTable: "Login_Data",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Timed_Tasks_Software_Data_softwareId",
                        column: x => x.softwareId,
                        principalTable: "Software_Data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Databases_UserIdFk",
                table: "Databases",
                column: "UserIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Data_userId",
                table: "Inventory_Data",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_Data_DatabaseId",
                table: "Software_Data",
                column: "DatabaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Timed_Tasks_softwareId",
                table: "Timed_Tasks",
                column: "softwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Timed_Tasks_userId",
                table: "Timed_Tasks",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory_Data");

            migrationBuilder.DropTable(
                name: "Timed_Tasks");

            migrationBuilder.DropTable(
                name: "Software_Data");

            migrationBuilder.DropTable(
                name: "Databases");

            migrationBuilder.DropTable(
                name: "Login_Data");
        }
    }
}
