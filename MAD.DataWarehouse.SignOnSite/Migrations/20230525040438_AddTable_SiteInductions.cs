using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAD.DataWarehouse.SignOnSite.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_SiteInductions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteInduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteCompany_Id = table.Column<int>(type: "int", nullable: true),
                    SiteCompany_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteInduction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompletedInduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SiteInductionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusSetBy_Id = table.Column<int>(type: "int", nullable: true),
                    StatusSetBy_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusSetBy_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusSetAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedInduction", x => new { x.SiteInductionId, x.Id });
                    table.ForeignKey(
                        name: "FK_CompletedInduction_SiteInduction_SiteInductionId",
                        column: x => x.SiteInductionId,
                        principalTable: "SiteInduction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnsubmittedForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SiteInductionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Offsite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsubmittedForm", x => new { x.SiteInductionId, x.Id });
                    table.ForeignKey(
                        name: "FK_UnsubmittedForm_SiteInduction_SiteInductionId",
                        column: x => x.SiteInductionId,
                        principalTable: "SiteInduction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedInduction");

            migrationBuilder.DropTable(
                name: "UnsubmittedForm");

            migrationBuilder.DropTable(
                name: "SiteInduction");
        }
    }
}
