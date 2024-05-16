using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "datecreate",
                table: "affairs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 17, 0, 57, 39, 751, DateTimeKind.Local).AddTicks(9326),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 15, 0, 39, 0, 202, DateTimeKind.Local).AddTicks(4609));

            migrationBuilder.CreateTable(
                name: "refreshtoken",
                columns: table => new
                {
                    userid = table.Column<Guid>(type: "TEXT", nullable: false),
                    refreshtoken = table.Column<string>(type: "TEXT", nullable: false),
                    datetimeexpires = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValue: new DateTime(2024, 5, 17, 21, 57, 39, 752, DateTimeKind.Utc).AddTicks(3863)),
                    datetimecreate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValue: new DateTime(2024, 5, 16, 21, 57, 39, 752, DateTimeKind.Utc).AddTicks(4252))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshtoken", x => x.userid);
                    table.ForeignKey(
                        name: "FK_refreshtoken_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refreshtoken");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datecreate",
                table: "affairs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 15, 0, 39, 0, 202, DateTimeKind.Local).AddTicks(4609),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 5, 17, 0, 57, 39, 751, DateTimeKind.Local).AddTicks(9326));
        }
    }
}
