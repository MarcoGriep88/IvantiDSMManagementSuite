using Microsoft.EntityFrameworkCore.Migrations;

namespace Packless.Migrations
{
    public partial class tours_pflichtfelder_deklariert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tours",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RandomAccessKey",
                table: "Tours",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tours",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RandomAccessKey",
                table: "Tours",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
