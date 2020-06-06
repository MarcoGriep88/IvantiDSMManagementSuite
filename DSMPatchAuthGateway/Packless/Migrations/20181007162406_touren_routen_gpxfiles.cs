using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Packless.Migrations
{
    public partial class touren_routen_gpxfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GPXFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 250, nullable: false),
                    UploadedFilePath = table.Column<string>(nullable: false),
                    TourRouteId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPXFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    RouteDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    GPXFileId = table.Column<int>(nullable: true),
                    TourId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRoutes_GPXFiles_GPXFileId",
                        column: x => x.GPXFileId,
                        principalTable: "GPXFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    TourDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    TourRouteId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_TourRoutes_TourRouteId",
                        column: x => x.TourRouteId,
                        principalTable: "TourRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourRoutes_GPXFileId",
                table: "TourRoutes",
                column: "GPXFileId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRoutes_TourId",
                table: "TourRoutes",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourRouteId",
                table: "Tours",
                column: "TourRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourRoutes_Tours_TourId",
                table: "TourRoutes",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourRoutes_GPXFiles_GPXFileId",
                table: "TourRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_TourRoutes_Tours_TourId",
                table: "TourRoutes");

            migrationBuilder.DropTable(
                name: "GPXFiles");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "TourRoutes");
        }
    }
}
