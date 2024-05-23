using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimeexpires",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 23, 21, 0, 2, 563, DateTimeKind.Utc).AddTicks(3446),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 17, 21, 57, 39, 752, DateTimeKind.Utc).AddTicks(3863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimecreate",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 23, 20, 57, 2, 563, DateTimeKind.Utc).AddTicks(3783),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 16, 21, 57, 39, 752, DateTimeKind.Utc).AddTicks(4252));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datecreate",
                table: "affairs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 23, 23, 57, 2, 562, DateTimeKind.Local).AddTicks(9341),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 17, 0, 57, 39, 751, DateTimeKind.Local).AddTicks(9326));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimeexpires",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 17, 21, 57, 39, 752, DateTimeKind.Utc).AddTicks(3863),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 23, 21, 0, 2, 563, DateTimeKind.Utc).AddTicks(3446));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimecreate",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 16, 21, 57, 39, 752, DateTimeKind.Utc).AddTicks(4252),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 23, 20, 57, 2, 563, DateTimeKind.Utc).AddTicks(3783));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datecreate",
                table: "affairs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 17, 0, 57, 39, 751, DateTimeKind.Local).AddTicks(9326),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 23, 23, 57, 2, 562, DateTimeKind.Local).AddTicks(9341));
        }
    }
}
