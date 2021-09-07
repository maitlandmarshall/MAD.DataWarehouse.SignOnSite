using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAD.DataWarehouse.SignOnSite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SignonAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SignoffAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Company_Id = table.Column<int>(type: "int", nullable: true),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisitor = table.Column<bool>(type: "bit", nullable: false),
                    IsInductedToSite = table.Column<bool>(type: "bit", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    User_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteAttendance_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteBriefing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AcknowledgementCount = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    SeenCount = table.Column<int>(type: "int", nullable: false),
                    ShouldShowSetByUser = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteBriefing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteBriefing_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BriefingLog",
                columns: table => new
                {
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EarliestAcknowledgedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Day = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BriefingId = table.Column<int>(type: "int", nullable: false),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EarliestSeenAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BriefingLog", x => new { x.BriefingId, x.FirstName, x.LastName, x.Day, x.EarliestAcknowledgedAt });
                    table.ForeignKey(
                        name: "FK_BriefingLog_SiteBriefing_BriefingId",
                        column: x => x.BriefingId,
                        principalTable: "SiteBriefing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttendance_SiteId",
                table: "SiteAttendance",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteBriefing_SiteId",
                table: "SiteBriefing",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BriefingLog");

            migrationBuilder.DropTable(
                name: "SiteAttendance");

            migrationBuilder.DropTable(
                name: "SiteBriefing");

            migrationBuilder.DropTable(
                name: "Site");
        }
    }
}
