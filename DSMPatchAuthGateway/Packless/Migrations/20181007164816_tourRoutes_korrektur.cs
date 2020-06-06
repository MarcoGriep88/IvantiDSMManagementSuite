using Microsoft.EntityFrameworkCore.Migrations;

namespace Packless.Migrations
{
    public partial class tourRoutes_korrektur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomAccessKey",
                table: "TourRoutes");

            migrationBuilder.AddColumn<string>(
                name: "RandomAccessKey",
                table: "Tours",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomAccessKey",
                table: "Tours");

            migrationBuilder.AddColumn<string>(
                name: "RandomAccessKey",
                table: "TourRoutes",
                nullable: true);
        }
    }
}
