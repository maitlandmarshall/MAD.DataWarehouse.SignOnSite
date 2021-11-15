using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAD.DataWarehouse.SignOnSite.Migrations
{
    public partial class AddSiteAttendeeApiTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteAttendance");

            migrationBuilder.CreateTable(
                name: "SiteAttendee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasActiveEnrolment = table.Column<bool>(type: "bit", nullable: false),
                    NeedsToDoSiteInduction = table.Column<bool>(type: "bit", nullable: false),
                    SiteInduction_Id = table.Column<long>(type: "bigint", nullable: true),
                    SiteInduction_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteInduction_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAttendee", x => new { x.Id, x.SiteId });
                });

            migrationBuilder.CreateTable(
                name: "SiteAttendeeAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SiteAttendeeId = table.Column<long>(type: "bigint", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    SignonAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SignoffAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    SignonChannel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BriefingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_Id = table.Column<int>(type: "int", nullable: true),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAttendeeAttendance", x => new { x.SiteAttendeeId, x.SiteId, x.Id });
                    table.ForeignKey(
                        name: "FK_SiteAttendeeAttendance_SiteAttendee_SiteAttendeeId_SiteId",
                        columns: x => new { x.SiteAttendeeId, x.SiteId },
                        principalTable: "SiteAttendee",
                        principalColumns: new[] { "Id", "SiteId" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteAttendeeAttendance");

            migrationBuilder.DropTable(
                name: "SiteAttendee");

            migrationBuilder.CreateTable(
                name: "SiteAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    IsInductedToSite = table.Column<bool>(type: "bit", nullable: false),
                    IsVisitor = table.Column<bool>(type: "bit", nullable: false),
                    SignoffAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    SignonAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    Company_Id = table.Column<int>(type: "int", nullable: true),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    User_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttendance_SiteId",
                table: "SiteAttendance",
                column: "SiteId");
        }
    }
}
