using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenToUserManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Leaves",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 1, 14, 17, 28, 44, 561, DateTimeKind.Local).AddTicks(3022), new DateTime(2025, 1, 11, 17, 28, 44, 560, DateTimeKind.Local).AddTicks(2347) });

            migrationBuilder.UpdateData(
                table: "Leaves",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 1, 21, 17, 28, 44, 561, DateTimeKind.Local).AddTicks(3983), new DateTime(2025, 1, 16, 17, 28, 44, 561, DateTimeKind.Local).AddTicks(3972) });

            migrationBuilder.UpdateData(
                table: "Payrolls",
                keyColumn: "Id",
                keyValue: 1,
                column: "PayDate",
                value: new DateTime(2025, 1, 11, 17, 28, 44, 561, DateTimeKind.Local).AddTicks(6028));

            migrationBuilder.UpdateData(
                table: "Payrolls",
                keyColumn: "Id",
                keyValue: 2,
                column: "PayDate",
                value: new DateTime(2025, 1, 11, 17, 28, 44, 561, DateTimeKind.Local).AddTicks(6367));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RefreshToken",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RefreshToken",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Leaves",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 1, 14, 16, 48, 54, 857, DateTimeKind.Local).AddTicks(9812), new DateTime(2025, 1, 11, 16, 48, 54, 856, DateTimeKind.Local).AddTicks(4919) });

            migrationBuilder.UpdateData(
                table: "Leaves",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 1, 21, 16, 48, 54, 858, DateTimeKind.Local).AddTicks(1244), new DateTime(2025, 1, 16, 16, 48, 54, 858, DateTimeKind.Local).AddTicks(1229) });

            migrationBuilder.UpdateData(
                table: "Payrolls",
                keyColumn: "Id",
                keyValue: 1,
                column: "PayDate",
                value: new DateTime(2025, 1, 11, 16, 48, 54, 858, DateTimeKind.Local).AddTicks(4435));

            migrationBuilder.UpdateData(
                table: "Payrolls",
                keyColumn: "Id",
                keyValue: 2,
                column: "PayDate",
                value: new DateTime(2025, 1, 11, 16, 48, 54, 858, DateTimeKind.Local).AddTicks(4995));
        }
    }
}
