using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGCalendar.Data.Migrations.SqlServerMigrations
{
    public partial class UserAndItemChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GameItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameItems");
        }
    }
}
