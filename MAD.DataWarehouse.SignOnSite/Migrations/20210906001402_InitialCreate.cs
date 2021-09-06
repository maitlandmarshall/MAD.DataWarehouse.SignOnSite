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
                    Id = table.Column<long>(type: "bigint", nullable: false),
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
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    SiteId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteAttendance_Site_SiteId1",
                        column: x => x.SiteId1,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttendance_SiteId1",
                table: "SiteAttendance",
                column: "SiteId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteAttendance");

            migrationBuilder.DropTable(
                name: "Site");
        }
    }
}
