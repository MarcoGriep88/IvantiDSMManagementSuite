using Microsoft.EntityFrameworkCore.Migrations;

namespace Packless.Migrations
{
    public partial class remove_correctedWeight_from_Gear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectedWeight",
                table: "Gear");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectedWeight",
                table: "Gear",
                nullable: false,
                defaultValue: 0);
        }
    }
}
