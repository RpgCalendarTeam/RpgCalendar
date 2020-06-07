using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGCalendar.Data.Migrations.SqlServerMigrations
{
    public partial class UpdateCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Calendars_GameTimeId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameTimeId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameTimeId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "DaysInWeek",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "Months",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "MonthsInYear",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "WeeksInMonth",
                table: "Calendars");

            migrationBuilder.AddColumn<string>(
                name: "PlayerBio",
                table: "GameUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerClass",
                table: "GameUser",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalendarId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CalendarId1",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrentTime",
                table: "Calendars",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DayLength",
                table: "Calendars",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Calendars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "HourLength",
                table: "Calendars",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    LengthInDays = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Months_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_CalendarId1",
                table: "Games",
                column: "CalendarId1");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_GameId",
                table: "Calendars",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Days_CalendarId",
                table: "Days",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Months_CalendarId",
                table: "Months",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Games_GameId",
                table: "Calendars",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Calendars_CalendarId1",
                table: "Games",
                column: "CalendarId1",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Games_GameId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Calendars_CalendarId1",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropIndex(
                name: "IX_Games_CalendarId1",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Calendars_GameId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "PlayerBio",
                table: "GameUser");

            migrationBuilder.DropColumn(
                name: "PlayerClass",
                table: "GameUser");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CalendarId1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CurrentTime",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "DayLength",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "HourLength",
                table: "Calendars");

            migrationBuilder.AddColumn<int>(
                name: "GameTimeId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Calendars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DaysInWeek",
                table: "Calendars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Months",
                table: "Calendars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthsInYear",
                table: "Calendars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "Calendars",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WeeksInMonth",
                table: "Calendars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameTimeId",
                table: "Games",
                column: "GameTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Calendars_GameTimeId",
                table: "Games",
                column: "GameTimeId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
