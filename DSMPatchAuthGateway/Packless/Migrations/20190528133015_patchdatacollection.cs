using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Packless.Migrations
{
    public partial class patchdatacollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourRoutes_GPXFiles_GPXFileId",
                table: "TourRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_TourRoutes_Tours_TourId",
                table: "TourRoutes");

            migrationBuilder.DropTable(
                name: "Gear");

            migrationBuilder.DropTable(
                name: "GPXFiles");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "TourRoutes");

            migrationBuilder.CreateTable(
                name: "PatchDataCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Computer = table.Column<string>(nullable: true),
                    Patch = table.Column<string>(nullable: true),
                    Compliance = table.Column<string>(nullable: true),
                    FoundDate = table.Column<DateTime>(nullable: false),
                    FixDate = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatchDataCollection", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatchDataCollection");

            migrationBuilder.CreateTable(
                name: "Gear",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Classification = table.Column<int>(nullable: false),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPXFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 250, nullable: false),
                    TourRouteId = table.Column<int>(nullable: true),
                    UploadedFilePath = table.Column<string>(nullable: false),
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
                    EndTime = table.Column<DateTime>(nullable: false),
                    GPXFileId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    RouteDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
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
                    EndDate = table.Column<DateTime>(nullable: false),
                    RandomAccessKey = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    TourDescription = table.Column<string>(nullable: true),
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
    }
}
