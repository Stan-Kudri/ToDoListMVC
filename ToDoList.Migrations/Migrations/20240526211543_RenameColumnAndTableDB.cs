using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnAndTableDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refreshtoken_user_userid",
                table: "refreshtoken");

            migrationBuilder.DropTable(
                name: "affairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshtoken",
                table: "refreshtoken");

            migrationBuilder.RenameTable(
                name: "refreshtoken",
                newName: "refreshTokens");

            migrationBuilder.RenameColumn(
                name: "refreshtoken",
                table: "refreshTokens",
                newName: "refreshToken");

            migrationBuilder.RenameColumn(
                name: "datetimeexpires",
                table: "refreshTokens",
                newName: "dateTimeExpires");

            migrationBuilder.RenameColumn(
                name: "datetimecreate",
                table: "refreshTokens",
                newName: "dateTimeCreate");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "refreshTokens",
                newName: "userId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeExpires",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 21, 15, 42, 990, DateTimeKind.Utc).AddTicks(7920),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 27, 18, 20, 19, 208, DateTimeKind.Utc).AddTicks(4289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTimeCreate",
                table: "refreshTokens",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 21, 15, 42, 990, DateTimeKind.Utc).AddTicks(8338),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 26, 18, 20, 19, 208, DateTimeKind.Utc).AddTicks(4778));

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens",
                column: "userId");

            migrationBuilder.CreateTable(
                name: "toDoItems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    dateCreate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 5, 27, 0, 15, 42, 991, DateTimeKind.Local).AddTicks(5921)),
                    isCompletion = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false),
                    dataCompletion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toDoItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_toDoItems_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_toDoItems_UserId",
                table: "toDoItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshTokens_user_userId",
                table: "refreshTokens",
                column: "userId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refreshTokens_user_userId",
                table: "refreshTokens");

            migrationBuilder.DropTable(
                name: "toDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens");

            migrationBuilder.RenameTable(
                name: "refreshTokens",
                newName: "refreshtoken");

            migrationBuilder.RenameColumn(
                name: "refreshToken",
                table: "refreshtoken",
                newName: "refreshtoken");

            migrationBuilder.RenameColumn(
                name: "dateTimeExpires",
                table: "refreshtoken",
                newName: "datetimeexpires");

            migrationBuilder.RenameColumn(
                name: "dateTimeCreate",
                table: "refreshtoken",
                newName: "datetimecreate");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "refreshtoken",
                newName: "userid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimeexpires",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 18, 20, 19, 208, DateTimeKind.Utc).AddTicks(4289),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 27, 21, 15, 42, 990, DateTimeKind.Utc).AddTicks(7920));

            migrationBuilder.AlterColumn<DateTime>(
                name: "datetimecreate",
                table: "refreshtoken",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 18, 20, 19, 208, DateTimeKind.Utc).AddTicks(4778),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2024, 5, 26, 21, 15, 42, 990, DateTimeKind.Utc).AddTicks(8338));

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshtoken",
                table: "refreshtoken",
                column: "userid");

            migrationBuilder.CreateTable(
                name: "affairs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    datacompletion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    datecreate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 5, 26, 21, 20, 19, 207, DateTimeKind.Local).AddTicks(9198)),
                    description = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    iscompletion = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affairs", x => x.id);
                    table.ForeignKey(
                        name: "FK_affairs_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_affairs_UserId",
                table: "affairs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshtoken_user_userid",
                table: "refreshtoken",
                column: "userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
