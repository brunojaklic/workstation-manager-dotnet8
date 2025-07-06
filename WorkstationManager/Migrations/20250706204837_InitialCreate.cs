using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkstationManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkPositionName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkPositionDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPositions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserWorkPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkPositionId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkPositions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkPositions_WorkPositions_WorkPositionId",
                        column: x => x.WorkPositionId,
                        principalTable: "WorkPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleDescription", "RoleName" },
                values: new object[,]
                {
                    { 1, "Administrator with full access", "Admin" },
                    { 2, "Standard user with limited access", "User" }
                });

            migrationBuilder.InsertData(
                table: "WorkPositions",
                columns: new[] { "Id", "WorkPositionDescription", "WorkPositionName" },
                values: new object[,]
                {
                    { 1, "Software development position", "Developer" },
                    { 2, "UI/UX design position", "Designer" },
                    { 3, "Quality assurance position", "Tester" },
                    { 4, "Infrastructure and deployment position", "DevOps" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "$2a$11$zWb0MR5GL8u6ITn7wwkT..midrrrZGOPCTyHOHTSRMndmf7XsQYsO", 1, "johndoe" },
                    { 2, "Jane", "Smith", "$2a$11$Pk5CL5X7vCV3yHoo.TXu/edobRXniGaENgWx.ZTLNFuhadoxIkzx6", 2, "janesmith" },
                    { 3, "Robert", "Johnson", "$2a$11$MB8m0uCtkkZfsjQC1/tfZeXUJHgsznISVkN6hWySVB9101qmMOZ9m", 2, "rjohnson" },
                    { 4, "Emily", "Williams", "$2a$11$qsEiLsT0uLlpyHdsXiMJi.IlNCKBmGXi07q0Mo6mBdltHoSVxVhUG", 2, "ewilliams" }
                });

            migrationBuilder.InsertData(
                table: "UserWorkPositions",
                columns: new[] { "Id", "ProductName", "UserId", "WorkDate", "WorkPositionId" },
                values: new object[,]
                {
                    { 1, "ERP System", 1, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "CRM Platform", 1, new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 3, "Mobile App", 2, new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, "Data Analytics Tool", 3, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, "E-commerce Website", 4, new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkPositions_UserId",
                table: "UserWorkPositions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkPositions_WorkPositionId",
                table: "UserWorkPositions",
                column: "WorkPositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWorkPositions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkPositions");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
