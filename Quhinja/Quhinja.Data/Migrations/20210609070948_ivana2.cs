using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quhinja.Data.Migrations
{
    public partial class ivana2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "43c869cc-9413-446a-98a3-da28f5b36bf6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b9317086-e54a-4477-b053-31d04d062ead");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "DateOfEmployment" },
                values: new object[] { "42c7b150-fbd5-423d-a3b8-3ce5b3c2611b", new DateTime(2021, 6, 9, 9, 9, 45, 541, DateTimeKind.Local).AddTicks(7087), new DateTime(2021, 6, 9, 9, 9, 45, 554, DateTimeKind.Local).AddTicks(7287) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e91dd320-4b46-49eb-8f77-bca467fa6a84");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "41f8060c-c1a6-489e-b1a4-f408cec95e11");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "DateOfEmployment" },
                values: new object[] { "59f7c174-af69-4cd7-b927-7b8fa9154923", new DateTime(2021, 6, 9, 9, 7, 14, 813, DateTimeKind.Local).AddTicks(2061), new DateTime(2021, 6, 9, 9, 7, 14, 825, DateTimeKind.Local).AddTicks(8329) });
        }
    }
}
