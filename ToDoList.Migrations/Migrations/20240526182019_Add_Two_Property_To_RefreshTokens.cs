using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Add_Two_Property_To_RefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimeexpires",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 18, 20, 19, 208, DateTimeKind.Utc).AddTicks(4289),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 23, 21, 0, 2, 563, DateTimeKind.Utc).AddTicks(3446));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimecreate",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 18, 20, 19, 208, DateTimeKind.Utc).AddTicks(4778),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 23, 20, 57, 2, 563, DateTimeKind.Utc).AddTicks(3783));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datecreate",
                table: "affairs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 21, 20, 19, 207, DateTimeKind.Local).AddTicks(9198),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 23, 23, 57, 2, 562, DateTimeKind.Local).AddTicks(9341));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimeexpires",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 23, 21, 0, 2, 563, DateTimeKind.Utc).AddTicks(3446),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 27, 18, 20, 19, 208, DateTimeKind.Utc).AddTicks(4289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimecreate",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 23, 20, 57, 2, 563, DateTimeKind.Utc).AddTicks(3783),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 26, 18, 20, 19, 208, DateTimeKind.Utc).AddTicks(4778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datecreate",
                table: "affairs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 23, 23, 57, 2, 562, DateTimeKind.Local).AddTicks(9341),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 26, 21, 20, 19, 207, DateTimeKind.Local).AddTicks(9198));
        }
    }
}
