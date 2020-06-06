using Microsoft.EntityFrameworkCore.Migrations;

namespace Packless.Migrations
{
    public partial class tourRoutes_RandomAccessKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RandomAccessKey",
                table: "TourRoutes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomAccessKey",
                table: "TourRoutes");
        }
    }
}
