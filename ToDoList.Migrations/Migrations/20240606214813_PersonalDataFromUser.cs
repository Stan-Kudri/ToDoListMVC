using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class PersonalDataFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "birthDate",
                table: "user",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "user",
                type: "TEXT",
                nullable: false,
                defaultValue: "Not indicate");

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "user",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "user",
                type: "TEXT",
                nullable: false,
                defaultValue: "Not indicate");

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "user",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateCreate",
                table: "toDoItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 7, 0, 48, 13, 624, DateTimeKind.Local).AddTicks(9183),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 27, 23, 6, 10, 954, DateTimeKind.Local).AddTicks(5705));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeExpires",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 7, 21, 48, 13, 624, DateTimeKind.Utc).AddTicks(2424),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 28, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(495));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeCreate",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 6, 21, 48, 13, 624, DateTimeKind.Utc).AddTicks(2808),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 27, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(828));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthDate",
                table: "user");

            migrationBuilder.DropColumn(
                name: "country",
                table: "user");

            migrationBuilder.DropColumn(
                name: "firstName",
                table: "user");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "user");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "user");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateCreate",
                table: "toDoItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 23, 6, 10, 954, DateTimeKind.Local).AddTicks(5705),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 6, 7, 0, 48, 13, 624, DateTimeKind.Local).AddTicks(9183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeExpires",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 28, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(495),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 6, 7, 21, 48, 13, 624, DateTimeKind.Utc).AddTicks(2424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeCreate",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(828),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 6, 6, 21, 48, 13, 624, DateTimeKind.Utc).AddTicks(2808));
        }
    }
}
