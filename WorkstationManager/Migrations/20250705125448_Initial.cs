using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkstationManager.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleDescription",
                value: "Administrator with full access");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleDescription",
                value: "Standard user with limited access");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName", "Password", "Username" },
                values: new object[] { "John", "Doe", "$2a$11$Xj64eWw0hRmN.DyyLKjZA.Tze82i/Kn0gz.sElGNN6VREDmpd/qFW", "johndoe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName", "Password", "Username" },
                values: new object[] { "Jane", "Smith", "$2a$11$WbdArPUeEn3kGEvYG/790.hDATZu1yxcGmWBBQqqMxGd2U4oLXc2S", "janesmith" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 3, "Robert", "Johnson", "$2a$11$id2aFqUXxZIL5WTwnjiAVOFaCYD7Q6o/QQhZRtuUYSAUMXh2M7wUC", 2, "rjohnson" },
                    { 4, "Emily", "Williams", "$2a$11$ZwusYwThnihgEC6G.d3.LOVx6l2gx.VTDYcqi7bLxmRGRxKoBJ092", 2, "ewilliams" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserWorkPositions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserWorkPositions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserWorkPositions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserWorkPositions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserWorkPositions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WorkPositions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkPositions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkPositions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkPositions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName", "Password", "Username" },
                values: new object[] { null, null, "$2a$11$uHBvL/RXL96jvLOWCUpUM.HF5xVri0xwl4Z5BmNX7tYbDEZY4RWfu", "dario123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName", "Password", "Username" },
                values: new object[] { null, null, "$2a$11$OfdogtR3eaegX/MJ5vNm8OXMhGH.zA7AACzQMObDwNzvm.c7Aein.", "pero3" });
        }
    }
}
