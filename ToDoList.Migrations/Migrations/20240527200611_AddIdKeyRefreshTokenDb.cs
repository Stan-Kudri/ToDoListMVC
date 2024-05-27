using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddIdKeyRefreshTokenDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateCreate",
                table: "toDoItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 23, 6, 10, 954, DateTimeKind.Local).AddTicks(5705),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 27, 0, 15, 42, 991, DateTimeKind.Local).AddTicks(5921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeExpires",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 28, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(495),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 27, 21, 15, 42, 990, DateTimeKind.Utc).AddTicks(7920));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeCreate",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(828),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 26, 21, 15, 42, 990, DateTimeKind.Utc).AddTicks(8338));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "refreshTokens",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_refreshTokens_userId",
                table: "refreshTokens",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_refreshTokens_userId",
                table: "refreshTokens");

            migrationBuilder.DropColumn(
                name: "id",
                table: "refreshTokens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateCreate",
                table: "toDoItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 0, 15, 42, 991, DateTimeKind.Local).AddTicks(5921),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 27, 23, 6, 10, 954, DateTimeKind.Local).AddTicks(5705));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeExpires",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 21, 15, 42, 990, DateTimeKind.Utc).AddTicks(7920),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 28, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(495));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeCreate",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 21, 15, 42, 990, DateTimeKind.Utc).AddTicks(8338),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 27, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(828));

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens",
                column: "userId");
        }
    }
}
